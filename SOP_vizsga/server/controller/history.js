import Mysql from "../core/mysql.js";

class HistoryController
{
    static HISTORY_ERROR = "No data correspondig to this user in database!";

    static History(req, res, next)
    {
        const query = "select p_id, plate, price, DATE_FORMAT(date, '%Y-%m-%d %h:%m') as date from payments where u_id = ? order by date";
        const params = [res.userID];

        Mysql.SQL(query, params, (error, results, fields) => {
            if (results.length > 0)
            {
                res.history = results;
                next();
            }
            else
            {
                res.success = false;
                res.message = HistoryController.HISTORY_ERROR;
                next();
            }
        })

    }
}


export default HistoryController;