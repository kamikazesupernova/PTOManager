using System.Text.Json;
using PTOManager.Domain.Models;
using PTOManager.Domain.Services.Interfaces;


namespace PTOManager.Services;
public class RequestService : IRequestService
    {
        private const string jsonfile = "Requests.json";
        public RequestService()
        {
      
        }
    public IEnumerable<Request> List()
    {
        var JDoc = File.ReadAllText(jsonfile);

        var result = JsonSerializer.Deserialize<IEnumerable<Request>>(JDoc, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result;
    }
    public IEnumerable<Request> Find(int id)
    {
        var JDoc = File.ReadAllText(jsonfile);

        var result = JsonSerializer.Deserialize<IEnumerable<Request>>(JDoc, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (result == null)
                throw new ArgumentNullException($"Something went wrong: No requests found.");

        return result.Where(c => c.UserId == id);
       
    }
    public string Add(Request request)
    {

        var jDoc = File.ReadAllText(jsonfile);
    
         //Find out if we have requests stored to add to
        if(!String.IsNullOrWhiteSpace(jDoc)){        

            var requests = JsonSerializer.Deserialize<List<Request>>(jDoc, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (requests == null)
                    throw new ArgumentNullException($"Something went wrong: Cannot add item.");
                requests.Add(request);
                    var json = JsonSerializer.Serialize(requests,  new JsonSerializerOptions {
                WriteIndented = true
            });     

            File.WriteAllText(jsonfile, json); 
            return "OK"; 
        
        }
        else
        {
            //if not make new list to write to
            var requests = new List<Request>();
            requests.Add(request);

            var json = JsonSerializer.Serialize(requests,  new JsonSerializerOptions {
             WriteIndented = true
             });     

            File.WriteAllText(jsonfile, json); 
            return "OK";         

        }        
          
    }
}