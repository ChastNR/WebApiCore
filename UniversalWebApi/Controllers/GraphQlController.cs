using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using GraphQL.Types;
using DataRepository.GraphQL;
using DataRepository.GraphQL.Query;

namespace UniversalWebApi.Controllers
{
    [Route("[controller]")]
    public class GraphQlController : Controller
    {
        private readonly IDocumentExecuter _executer;
        private readonly ISchema _schema;

        public GraphQlController(IDependencyResolver resolver, IDocumentExecuter executer)
        {
            _executer = executer;
            _schema = new AppSchema(resolver);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}