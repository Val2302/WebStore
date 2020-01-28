using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Dto
{
    public class SaveResult
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
