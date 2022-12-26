import Server from "./core/server.js";

class Program
{
    constructor()
    {
        Program.Main();
    }

    static Main()
    {
        const SERVER = new Server();
    }
}

const PROGRAM = new Program();