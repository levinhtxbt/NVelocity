using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVelocity.Tool
{
    public interface ITemplateLoader
    {
	    string GetTemplate(string html);

	    Task<string> GetTemplateAsync(string html);
	}
}
