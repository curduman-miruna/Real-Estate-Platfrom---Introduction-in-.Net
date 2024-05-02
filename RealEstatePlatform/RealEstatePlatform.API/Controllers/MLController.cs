using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;

[ApiController]
[Route("[controller]")]
public class PredictionController : ControllerBase
{
    private readonly PredictionEnginePool<RealEstatePlatform_Model.ModelInput, RealEstatePlatform_Model.ModelOutput> _predictionEnginePool;

    public PredictionController(PredictionEnginePool<RealEstatePlatform_Model.ModelInput, RealEstatePlatform_Model.ModelOutput> predictionEnginePool)
    {
        _predictionEnginePool = predictionEnginePool;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Predict([FromBody] RealEstatePlatform_Model.ModelInput input)
    {
        var prediction = _predictionEnginePool.Predict(input);
        return Ok(prediction);
    }
}
