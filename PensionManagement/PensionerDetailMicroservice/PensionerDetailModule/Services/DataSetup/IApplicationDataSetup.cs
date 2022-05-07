using PensionerDetailModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionerDetailModule.Services.DataSetup
{
    public interface IApplicationDataSetup
    {
        List<PensionerDetail> GetPensionerDetails();
    }
}
