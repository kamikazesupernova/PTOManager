using System;
using System.Text.Json;
using PTOManager.Domain.Models;
using PTOManager.Services;

namespace PTOManager;

class Program
{
  public static RequestService requestClient = new RequestService();
  

    static void Main(string[] args)
    {
        int role = 0;      

       
            role = MainMenu();
            //range check 1-3
            if(!(role >= 1 && role <= 3)){

                Console.WriteLine("Invalid option!");
                return;        

            }
            //TODO: some type of user and password authorization
            //if(!ValidateUser()){
            //     Console.WriteLine("Access Denied!");
            //     return;    
            // }

            switch (role)
            {
                case 1:
                    EmployeeOption();
                    break;

                case 2:
                    AdminOption();
                    break;
                case 3:
                  return;                
                 
                default:                     
                    break;
            }

       
    }

    private static void AdminOption()
    {
        int option = 0;
        try
        {  
            do{
   
            Console.WriteLine("1. Approve Requests");      
            Console.WriteLine("2. Exit");

            var role = Console.ReadLine();

            if(!int.TryParse(role, out option))
            {
              Console.WriteLine("Invalid Input!");
              
            }

            if (option == 1){
                ApproveRequests();
             }


            } while(option!=2);
          

        }
        catch (System.Exception)
        {
            
             Console.WriteLine("Invalid Input!");
        }
    }

    private static void ApproveRequests()
    {
       int option = 0;
       try
       {
            var requests = requestClient.List().ToList();
            if (requests == null){                
             Console.WriteLine("No Requests to Approve");
             return;
            }
            else{                
                

                    foreach(var req in requests.Where(r =>r.Approved == null)){
                          Console.WriteLine(String.Format("{0}. UserId: {1} Reason {2}",
                         req.Id, req.UserId, req.Description));

                    }
                       
                do{
                    Console.WriteLine("Enter request number to approve (-1 to Exit): ");
                    var opt = Console.ReadLine();

                    if(!int.TryParse(opt, out option))
                    {
                        Console.WriteLine("Invalid Input!");
                        continue;                       
                    
                    }
                    //TODO: Validate option and Put Request to update request approved property
                } while(option!=-1);
           
            }
            
       }
       catch (System.Exception)
       {
        
        throw;
       }
    }

    private static int MainMenu()
    {  
        int result = 0;
        try
        {            

            Console.WriteLine("PTOManager v1.0");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Employee");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Exit");

            var role = Console.ReadLine();

            if(!int.TryParse(role, out result))
            {
              Console.WriteLine("Invalid Input!");
              
            }
        
        }
        catch (System.Exception)
        {
           //In production you would log ex 
           Console.WriteLine("Invalid Input!");
        }

        return result;

    }
    private static void EmployeeOption(){
        int option = 0;
        try
        {  
            do{
   
            Console.WriteLine("1. Add a Request");
            Console.WriteLine("2. View Requests");
            Console.WriteLine("3. Exit");

            var role = Console.ReadLine();

            if(!int.TryParse(role, out option))
            {
              Console.WriteLine("Invalid Input!");
              
            }

            switch (option)
            {
                case 1:
                    AddRequest(); 
                    break;

                case 2:
                    ViewEmployeeRequests(); 
                    break; 
                default: 
                     
                    break;
            }

            } while(option!=3);
          

        }
        catch (System.Exception)
        {
            
             Console.WriteLine("Invalid Input!");
        }
    }

    private static void AddRequest()
    {
        DateTime srtDate;
        DateTime endDate;
        Random rnd = new Random();        
        try
        {  
            //TODO Add input validator class 
            Console.WriteLine("Enter StartDate DD/MM/YYYY:");
            var startInput = Console.ReadLine();
            DateTime.TryParse(startInput, out srtDate);

            Console.WriteLine("Enter EndDate DD/MM/YYYY:");
            var endInput = Console.ReadLine();
            DateTime.TryParse(endInput, out endDate);

            Console.WriteLine("Enter your request reason:");
            var reasonInput = Console.ReadLine();

            var request = new Request{

                Id = rnd.Next(),
                UserId =  rnd.Next(),
                Date = DateTime.Now, 
                StartDate = srtDate,
                EndDate = endDate, 
                Description = reasonInput,
                Approved = null
                
              };


            requestClient.Add(request);
        }
        catch (System.Exception ex){
            
            Console.WriteLine(ex);
        }         
    }
    private static void ViewEmployeeRequests(){
     
        try
        {
            var requests = requestClient.Find(2023493778).ToList();
            foreach (var req  in requests)
            {              
    
                Console.WriteLine(String.Format("{0}. Reason: {1} Approved: {2}",
                         req.Id, req.Description, req.Approved == null?"Pending":req.Approved));
            } 
            
        }
        catch (System.Exception)
        {
            
            throw;
        }

    }
}