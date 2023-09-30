using System.Xml.Linq;
using ABPWebApi.Models;
using ABPWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABPWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExperimentController : ControllerBase
    {
        private const string buttonColor = "button-color";
        private const string buttonColorName = "GetButtonColor";
        private const string price = "price";
        private const string priceName = "GetPrice";
        private const string statistic = "statistic";
        private const string statisticName = "GetStatistic";

        private readonly IDataBase dataBase;

        public ExperimentController(IDataBase dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpGet(buttonColor, Name = buttonColorName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<KeyValuePair<XName, string>> GetButtonColor(string deviceToken)
        {
            try
            {
                string Value = dataBase.GetButtonColor(deviceToken);
                KeyValuePair<XName, string> result = new("button-color", Value);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet(price, Name = priceName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<KeyValuePair<XName, string>> GetPrice(string deviceToken)
        {
            try
            {
                string Value = dataBase.GetPrice(deviceToken);
                KeyValuePair<XName, string> result = new("price", Value);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet(statistic, Name = statisticName)]
        public Experiment[] GetStatistic()
        {
            return dataBase.GetStatistic();
        }
    }
}