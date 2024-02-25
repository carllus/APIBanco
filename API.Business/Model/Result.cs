using System.ComponentModel.DataAnnotations;

namespace API.Business.Model
{
    public class Result
    {
        public Result(bool success, string? message, string? desc) 
        {
            this.sucess = success;
            this.message = message;
            this.desc = desc;
        }
        public bool sucess { get; set; }
        public string? message { get; set; }
        public string? desc { get; set; }
    }
}
