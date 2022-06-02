using Microsoft.AspNetCore.Mvc;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI.Data.Models;
using GameReviewsAPI.Domain.Repositories;
using GameReviewsAPI.Domain.Services;
using GameReviewsAPI.Data.Dto;
using System.Linq;
using AutoMapper;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace GameReviewsAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с играми и рецензиями к ним
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GameReviewsController : ControllerBase
    {
        private IRepository<Game> _gamesDb;
        private IRepository<Review> _reviewsDb;
        private readonly IMapper _mapper;
        public GameReviewsController(IRepository<Game> gameRepository, IRepository<Review> reviewRepository, IMapper mapper)
        {
            _gamesDb = gameRepository;
            _reviewsDb = reviewRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Возвращает список игр отсортированных по убыванию рейтинга
        /// </summary>
        /// <response code="200">Игры были успешно возвращены</response>
        /// <response code="404">Игры или рецензии не найдены</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "List<GameDto>", typeof(List<GameDto>))]
        [HttpGet("GetAllGamesSortRate")]
        public async Task<IActionResult> GetAllGames()
        {
            GetAllSortedService sortedService = new GetAllSortedService(_gamesDb, _reviewsDb, _mapper);
            var result = await sortedService.GetAllSortedDesc();
            if(result != null)
                return Ok(result);
            return NotFound();

        }

        /// <summary>
        /// Возвращает рецензии по Id игры
        /// </summary>
        /// <param name="gameId"></param>
        /// <response code="200">Рецензии были успешно возвращены</response>
        /// <response code="404">Игра с данным id не найдена</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "List<ReviewInfoDto>", typeof(List<ReviewInfoDto>))]
        [HttpGet("GetReviewsByGameId")]
        public async Task<IActionResult> GetReviews(int gameId)
        {
            GetReviewsService reviewsService = new GetReviewsService(_gamesDb, _reviewsDb, _mapper);
            var result = await reviewsService.GetReviewsByGameId(gameId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Возвращает игру по Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Игра была успешно возвращена</response>
        /// <response code="404">Игра с данным id не найдена</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "GameInfoDto", typeof(GameInfoDto))]
        [HttpGet("GetGameById")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await _gamesDb.GetById(id);
            if (game != null)
                return Ok(_mapper.Map<GameInfoDto>(game));
            return NotFound();
        }

        /// <summary>
        /// Добавляет игру
        /// </summary>
        /// <param name="gameDto"></param>
        /// <response code="200">Игра была успешно добавлена</response>
        /// <response code="400">Данная игра уже существует</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "Id", typeof(long))]
        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame(GameInfoDto gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            long? id = await _gamesDb.Add(game);
            if (id != null)
                return Ok(id);
            return BadRequest();
        }

        /// <summary>
        /// Добавляет рецензию
        /// </summary>
        /// <param name="review"></param>
        /// <response code="400">Игра не найдена</response>
        [SwaggerResponse((int)HttpStatusCode.OK, "Id", typeof(long))]
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(ReviewInfoDto review)
        {
            AddReviewService reviewService = new AddReviewService(_gamesDb, _reviewsDb);
            var result = await reviewService.GetReviewsByGameId(review);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        /// <summary>
        /// Удаляет игру
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Игра была успешно удалена</response>
        /// <response code="404">Удаляемая игра не найдена</response>
        [HttpDelete("DeleteGameById")]
        public async Task<IActionResult> DeleteGame(long id)
        {
            DeleteGameService deleteGameService = new DeleteGameService(_gamesDb, _reviewsDb);
            var result = await deleteGameService.DeleteGameById(id);
            if (result)
                return NoContent();
            return NotFound();
        }
    }
}
