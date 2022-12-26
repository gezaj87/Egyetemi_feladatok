import Fetch from "./Fetch";

class Auth
{
    static async auth(states, token)
    {
        const route = '/api/auth';
        const method = 'POST';
        const body = {token: token};

        try
        {
            const response = await Fetch(route, method, body);

            console.log(response)

            if (response.success)
            {
                console.log(response);
                states.setIsLoggedIn(response.success);
                return true;
            }
            else
            {
                states.setIsLoggedIn(response.success);
                console.log("You are not logged in!");
            }

        }
        catch(error)
        {
            console.log(error);
        }
    }
}

export default Auth.auth;