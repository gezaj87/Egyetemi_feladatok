import mysql2 from 'mysql2';



class Mysql
{
    static connection = null;

    static async Connect()
    {
        Mysql.connection.connect((err) => {
            if (err)
            {
                console.error('error connecting: ' + err.stack);
                return false;
            }

            console.log('connected as id ' + Mysql.connection.threadId);
            return true;

        })
    }

    static async TerminateConnection()
    {
        Mysql.connection.end((err) => {
            if (err)
            {
                console.log("Terminate Connection error: " + err.stack)
            }

            console.log(`connection.threadId (${Mysql.connection.threadId}): Terminated.`);
        });
    }

    static async Query(queryString, params, next)
    {
        Mysql.connection.query (queryString, params, (error, results, fields) => {
            // error will be an Error if one occurred during the query
            // results will contain the results of the query
            // fields will contain information about the returned results fields (if any)
            // console.log(results)
            return next(error, results, fields);
        });
    }

    static async SQL(queryString, params, next)
    {
        Mysql.connection = mysql2.createConnection({
            host     : process.env.HOST,
            user     : process.env.USER,
            password : process.env.PASSWD,
            database : process.env.DATABASE
        });

        const conn = await Mysql.Connect();
        const query = await Mysql.Query(queryString, params, next);
        const term = await Mysql.TerminateConnection();
    }
}

export default Mysql;