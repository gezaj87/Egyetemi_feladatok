import Mysql from "../core/mysql.js";

class CarsController
{
    static NO_CARS = "There is no car in database!";

    static Cars(req, res, next)
    {   


        const avaiableOnly = req.body.avaiableOnly;

        const query1 = "select * from cars order by avaiable desc";
        const query2 = "select * from cars where avaiable = ?";

        const query = avaiableOnly? query2 : query1;
        const params = avaiableOnly? [1] : [];

        Mysql.SQL(query, params, (error, results, fields)=> {
            
            if (results.length > 0)
            {
                
                res.cars = results;
                next();
            }
            else
            {
                res.success = false;
                res.message = CarsController.NO_CARS;
                next();
            }

        })
    }

}

export default CarsController;