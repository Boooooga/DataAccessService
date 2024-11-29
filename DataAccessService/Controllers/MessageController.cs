using DataAccessService.Data;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessService.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RabbitMQService _rabbitMQService;
        public MessageController(AppDbContext context, RabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }

        // Пример метода для отправки сообщения
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            _rabbitMQService.SendMessage(message); // Отправка сообщения в очередь
            return Ok(new { Message = "Message sent to RabbitMQ" });
        }

        // Пример метода для получения сообщений
        [HttpGet("receive")]
        public IActionResult ReceiveMessages()
        {
            _rabbitMQService.ReceiveMessages(); // Получение сообщений из очереди
            return Ok(new { Message = "Receiving messages from RabbitMQ" });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
