using SN.DTO.{FolderName};
using SN.Service.Services.{FolderName}.Interface;

namespace SN.API.Controllers.{FolderName}
{
    [EnableCors("AllowMVCOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class Customer4Controller : Controller
    {
        private readonly ICustomer4Service _customer4Service;
        private readonly IUserAuthentication _authentication;
        private readonly IConfiguration _configuration;
        private readonly LoggerUtility _logger;
        public string _ExportExcelLocation;
        public string ExportExcelLocation
        {
            get
            {
                _ExportExcelLocation = !string.IsNullOrWhiteSpace(_configuration.GetSection("AppSettings")["ExportExcel"]) ? _configuration.GetSection("AppSettings")["ExportExcel"] : string.Empty;
                return _ExportExcelLocation;
            }
        }
        public SurveyTypeController(LoggerUtility logger, IConfiguration configuration, ICustomer4Service customer4Service, IUserAuthentication authentication)
        {
            _logger = logger;
            _authentication = authentication;
            _customer4Service = customer4Service;
            _configuration = configuration;
        }

        #region Thread Category

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Customer4DTO customer4DTO)
        {
            try
            {
                customer4DTO.CreatedBy = _authentication.CurrentUserDetails.UserId;
                var result = _customer4Service.Add(customer4DTO);//passing the object to BLL to insert into database
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(Customer4DTO customer4DTO)
        {
            try
            {
                customer4DTO.ModifiedBy = _authentication.CurrentUserDetails.UserId;
                var result = _customer4Service.Update(customer4DTO);//passing the object to BLL to insert into database
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long id)
        {
            try
            {
                //var PortfolioData = _customer4Service.GetById(id);
                var result = _customer4Service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

        [HttpGet]
        [Route("GetAllActiveSurveyType")]
        public IActionResult GetAllActiveSurveyType(string lang)
        {
            try
            {
                return Ok(_customer4Service.GetAllActiveSurveyType(lang));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

       
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(long id)
        {
            try
            {
                return Ok(_customer4Service.GetById(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

        [HttpPost]
        [Route("GetSurveyTypeFilter")]
        public IActionResult GetSurveyTypeFilter(Customer4DTO customer4DTO)
        {
            try
            {
                return Ok(_customer4Service.GetSurveyTypeFilter(customer4DTO));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _authentication.CurrentUserDetails);
                return StatusCode(500, Resource.Resource.GeneralExceptionMessage);
            }
        }

        #endregion
    }
}
