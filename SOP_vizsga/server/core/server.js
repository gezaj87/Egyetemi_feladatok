import express from 'express';
import {config as dotenvConfig} from 'dotenv';
import RouterClass from './router.js';
import bodyParser from 'body-parser';

import swaggerUi from 'swagger-ui-express';
import swaggerDocument from './openapi.json' assert { type: 'json' };


class Server
{
    constructor()
    {
        dotenvConfig();
        const PORT = process.env.PORT;
        const APP = express();

        APP.listen(PORT, () => {
            console.log(`Listening on port ${PORT}`);
        })

        const ROUTER = new RouterClass();
        APP.use(bodyParser.json())
        APP.use('/api', RouterClass.ROUTER);
        APP.use("/api-docs", swaggerUi.serve, swaggerUi.setup(swaggerDocument));

        // APP.get('/b', (req, res) => { //Line 9
        //     res.write({ express: 'YOUR EXPRESS BACKEND IS CONNECTED TO REACT' }); //Line 10
        // });

        //make a /hello route that returns a json object with a message property
        APP.get('/hello', (req, res) => {
            res.json({ message: 'Hello World!' });
        });

        // ROUTER = express.Router();
        // export router


        


    }
}
// const app = express();
// const port = process.env.PORT || 5000;

// This displays message that the server running and listening to specified port
// app.listen(port, () => console.log(`Listening on port ${port}`)); //Line 6

// create a GET route
// app.get('/express_backend', (req, res) => { //Line 9
//   res.send({ express: 'YOUR EXPRESS BACKEND IS CONNECTED TO REACT' }); //Line 10
// });

export default Server;

//export ROUTER
