using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IHelper
    {
        public Task<string> GetAPI(string url, string token);
        public Task<string> PostAPI(string url, string token, object obj);
        public Task<string> PutAPI(string url, string token, object obj);
        public Task<string> DeleteAPI(string url, string token);
    }
}
