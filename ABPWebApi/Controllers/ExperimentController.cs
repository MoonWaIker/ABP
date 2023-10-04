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
        private readonly ILogger<ExperimentController> _logger;

        public ExperimentController(IServiceProvider serviceProvider)
        {
            dataBase = serviceProvider.GetRequiredService<IDataBase>();
            _logger = serviceProvider.GetRequiredService<ILogger<ExperimentController>>();
        }

        [HttpGet(buttonColor, Name = buttonColorName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KeyValuePair<XName, string>> GetButtonColor(string deviceToken)
        {
            try
            {
                string Value = dataBase.GetButtonColor(deviceToken);
                KeyValuePair<XName, string> result = new("button-color", Value);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet(price, Name = priceName)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<KeyValuePair<XName, int>> GetPrice(string deviceToken)
        {
            try
            {
                int Value = dataBase.GetPrice(deviceToken);
                KeyValuePair<XName, int> result = new("price", Value);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet(statistic, Name = statisticName)]
        public Experiments GetStatistic()
        {
            return dataBase.GetStatistic();
        }
    }
}