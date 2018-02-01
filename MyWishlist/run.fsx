#r "System.Net.Http"
#r "Newtonsoft.Json"

open System.Net
open System.Net.Http
open Newtonsoft.Json

type Wish = {
    text: string
    link: string
}

type Wishlist = {
    name: string
    wishes: Wish array
}

let Run(req: HttpRequestMessage, log: TraceWriter) =
    async {
        log.Info(sprintf 
            "F# HTTP trigger function processed a request.")

        let! data = req.Content.ReadAsStringAsync() |> Async.AwaitTask

        if not (String.IsNullOrEmpty(data)) then
            let wishlist = JsonConvert.DeserializeObject<Wishlist>(data)
            return req.CreateResponse(HttpStatusCode.OK, wishlist);
        else
            return req.CreateResponse(HttpStatusCode.BadRequest);
    } |> Async.RunSynchronously
