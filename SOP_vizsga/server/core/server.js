import express from 'express';
import {config as dotenvConfig} from 'dotenv';
import RouterClass from './router.js';
import bodyParser from 'body-parser';

import swaggerUi from 'swagger-ui-express';
import swaggerDocument from './openapi.json' assert { type: 'json' };


import cors from 'cors';

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
		APP.use(cors());
        APP.use(bodyParser.json())
        APP.use('/api', RouterClass.ROUTER);
        APP.use("/api-docs", swaggerUi.serve, swaggerUi.setup(swaggerDocument));

    }
}

export default Server;