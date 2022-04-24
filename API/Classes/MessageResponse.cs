using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Classes
{
    public class MessageResponse
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string IsSuccessfulMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;

        private bool _isSuccessfful = true;


        public bool IsSuccessfful
        {
            get 
            { 
                return _isSuccessfful;
            }
            set
            {
                _isSuccessfful = value;

                if (_isSuccessfful == false)
                {
                    IsSuccessfulMessage = "NO";
                }
                else
                {
                    IsSuccessfulMessage = "YES";
                }
                
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


       
    }
}
