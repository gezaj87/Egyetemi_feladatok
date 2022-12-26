class Fetch
{
    static async FetchApi(route, method, body)
    {
        const requestOptions = {
            method: method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(body)
        }

        const response = await fetch(route, requestOptions);
        const json = await response.json();
        return json;
    }
}


export default Fetch.FetchApi;