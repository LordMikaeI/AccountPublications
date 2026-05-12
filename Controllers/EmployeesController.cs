using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kursach_Backend.DbContexts;
using Kursach_Backend.Models.DTO;
using Kursach_Backend.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Kursach_Backend.Models.Publications;

namespace Kursach_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private ApplicationContext _context;

        public EmployeesController()
        {
            _context = new ApplicationContext();
        }

        [HttpGet("employees")]
        [Authorize]
        public IActionResult GetEmployees()
        {
            var employees = _context.Employees
                .Include(e => e.Publications)
                    .ThenInclude(p => p.PublicationEdition)
                .Include(e => e.PublicationFiles)
                .Select(e => new
                {
                    e.Id,
                    e.FirstName,
                    e.LastName,
                    e.MidleName,
                    e.Email,
                    e.PhoneNumber,
                    e.BirthDate,
                    Publications = e.Publications.Select(p => new
                    {
                        p.Id,
                        p.Title,
                        p.Description,
                        p.PublicationDate,
                        p.Status,
                        p.CreatedAt,
                        EditionName = p.PublicationEdition != null ? p.PublicationEdition.EditionName : null,
                        RINC = _context.ScientificPublications
                            .Where(sp => sp.PublicationId == p.Id)
                            .Select(sp => sp.RINC)
                            .FirstOrDefault(),
                        IsScientific = _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id),
                        PublicationType = _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Монография" :
                                         _context.Articles.Any(a => a.PublicationId == p.Id) ? "Статья" :
                                         _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                                         _context.Dissertations.Any(d => d.PublicationId == p.Id) ? "Диссертация" :
                                         _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Учебное пособие" :
                                         _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" : "Неизвестно"
                    }),
                    PublicationFiles = e.PublicationFiles.Select(f => new
                    {
                        f.Id,
                        f.DisplayName,
                        f.SystemName
                    })
                })
                .ToList();

            return Ok(employees);
        }


        [HttpPost("employee")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult CreateEmployee([FromBody] EmployeeRequestDto employeeDto)
        {
            var _employee = _context.Employees.Add(new Employee
            {
                BirthDate = employeeDto.BirthDate,
                Email = employeeDto.Email,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                MidleName = employeeDto.MidleName,
                PhoneNumber = employeeDto.PhoneNumber,
            });
            _context.SaveChanges();
            return Ok(_employee.Entity.Id);
        }

        [HttpPut("employee")]
        public IActionResult UpdateEmployee([FromBody] EmployeeRequestDto employeeDto, int id)
        {
            var _employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (_employee != null)
            {
                _employee.BirthDate = employeeDto.BirthDate;
                _employee.Email = employeeDto.Email;
                _employee.FirstName = employeeDto.FirstName;
                _employee.LastName = employeeDto.LastName;
                _employee.MidleName = employeeDto.MidleName;
                _employee.PhoneNumber = employeeDto.PhoneNumber;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Employee not found");
        }

        [HttpDelete("employee")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult DeleteEmployee(int id)
        {
            var _employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (_employee != null)
            {
                _context.Employees.Remove(_employee);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Employee not found");
        }

        [HttpGet("search")]
        [Authorize]
        public IActionResult SearchEmployees(string firstName, string midleName)
        {
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(midleName))
                return BadRequest("Укажите имя и фамилию для поиска");

            var query = _context.Employees
                .Include(e => e.Publications)
                    .ThenInclude(p => p.PublicationEdition)
                .Include(e => e.PublicationFiles)
                .AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(e => e.FirstName.ToLower().Contains(firstName.ToLower()));

            if (!string.IsNullOrEmpty(midleName))
                query = query.Where(e => e.MidleName.ToLower().Contains(midleName.ToLower()));

            var employees = query.Select(e => new
            {
                e.Id,
                e.FirstName,
                e.LastName,
                e.MidleName,
                e.Email,
                e.PhoneNumber,
                e.BirthDate,
                Publications = e.Publications.Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    p.PublicationDate,
                    p.Status,
                    p.CreatedAt,
                    EditionName = p.PublicationEdition != null ? p.PublicationEdition.EditionName : null,
                    RINC = _context.ScientificPublications
                        .Where(sp => sp.PublicationId == p.Id)
                        .Select(sp => sp.RINC)
                        .FirstOrDefault(),
                    IsScientific = _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id),
                    PublicationType = _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Монография" :
                                     _context.Articles.Any(a => a.PublicationId == p.Id) ? "Статья" :
                                     _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                                     _context.Dissertations.Any(d => d.PublicationId == p.Id) ? "Диссертация" :
                                     _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Учебное пособие" :
                                     _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" : "Неизвестно"
                }),
                PublicationFiles = e.PublicationFiles.Select(f => new
                {
                    f.Id,
                    f.DisplayName,
                    f.SystemName
                })
            })
            .ToList();

            if (!employees.Any())
                return NotFound("Сотрудники не найдены");

            return Ok(employees);
        }

        [HttpPost("publication")]
        public IActionResult AddPublication([FromBody] PublicationRequestDto publicationDto)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == publicationDto.EmployeeId);
            if (employee == null)
                return BadRequest("Employee not found");

            bool hasRINC = !string.IsNullOrEmpty(publicationDto.RINC);
            if (hasRINC && !IsValidRINC(publicationDto.RINC))
                return BadRequest("Неверный формат РИНЦ. Формат: XXXX-XXXX-XXXX");

            var validTypes = new[] { "Monograph", "Article", "Conference", "Dissertation", "Textbook", "Patent" };
            if (!validTypes.Contains(publicationDto.PublicationType))
                return BadRequest("Invalid publication type. Use: Monograph, Article, Conference, Dissertation, Textbook, Patent");

            var publication = new Publication
            {
                Title = publicationDto.Title,
                Description = publicationDto.Description,
                PublicationDate = publicationDto.PublicationDate,
                PublicationEditionId = publicationDto.PublicationEditionId,
                Status = publicationDto.Status ?? "published",
                CreatedAt = publicationDto.CreatedAt == default ? DateTime.UtcNow : publicationDto.CreatedAt,
                EmployeeId = publicationDto.EmployeeId
            };

            if (employee.Publications == null)
                employee.Publications = new List<Publication>();

            employee.Publications.Add(publication);
            _context.SaveChanges();

            switch (publicationDto.PublicationType)
            {
                case "Monograph":
                    _context.Monographs.Add(new Monograph
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
                case "Article":
                    _context.Articles.Add(new Article
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
                case "Conference":
                    _context.Conferences.Add(new Conference
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
                case "Dissertation":
                    _context.Dissertations.Add(new Dissertation
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
                case "Textbook":
                    _context.Textbooks.Add(new Textbook
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
                case "Patent":
                    _context.Patents.Add(new Patent
                    {
                        Title = publicationDto.Title,
                        Content = publicationDto.Description,
                        Status = publicationDto.Status ?? "published",
                        CreatedAt = publication.CreatedAt,
                        PublicationId = publication.Id
                    });
                    break;
            }

            if (hasRINC)
            {
                _context.ScientificPublications.Add(new ScientificPublication
                {
                    RINC = publicationDto.RINC,
                    PublicationId = publication.Id
                });
            }

            _context.SaveChanges();

            return Ok(new
            {
                id = publication.Id,
                isScientific = hasRINC,
                publicationType = publicationDto.PublicationType
            });
        }

        private bool IsValidRINC(string rinc)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(rinc, @"^\d{4}-\d{4}-\d{4}$");
        }

        [HttpDelete("publication")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult DeletePublication(int id)
        {
            var publication = _context.Publications.FirstOrDefault(p => p.Id == id);
            if (publication == null)
                return BadRequest("Publication not found");

            var monograph = _context.Monographs.FirstOrDefault(m => m.PublicationId == id);
            if (monograph != null) _context.Monographs.Remove(monograph);

            var article = _context.Articles.FirstOrDefault(a => a.PublicationId == id);
            if (article != null) _context.Articles.Remove(article);

            var conference = _context.Conferences.FirstOrDefault(c => c.PublicationId == id);
            if (conference != null) _context.Conferences.Remove(conference);

            var dissertation = _context.Dissertations.FirstOrDefault(d => d.PublicationId == id);
            if (dissertation != null) _context.Dissertations.Remove(dissertation);

            var textbook = _context.Textbooks.FirstOrDefault(t => t.PublicationId == id);
            if (textbook != null) _context.Textbooks.Remove(textbook);

            var patent = _context.Patents.FirstOrDefault(p => p.PublicationId == id);
            if (patent != null) _context.Patents.Remove(patent);

            var scientificPublication = _context.ScientificPublications.FirstOrDefault(sp => sp.PublicationId == id);
            if (scientificPublication != null) _context.ScientificPublications.Remove(scientificPublication);

            _context.Publications.Remove(publication);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("scientificpublication")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult DeleteScientificPublication(int id)
        {
            var scientificPublication = _context.ScientificPublications.FirstOrDefault(sp => sp.Id == id);
            if (scientificPublication == null)
                return BadRequest("Scientific publication not found");

            var publication = _context.Publications.FirstOrDefault(p => p.Id == scientificPublication.PublicationId);
            if (publication != null)
                _context.Publications.Remove(publication);

            _context.ScientificPublications.Remove(scientificPublication);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("publications/sorted")]
        [Authorize]
        public IActionResult GetPublicationsSorted(string sortBy = "Дата", string order = "Убывание")
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return BadRequest("Поле сортировки не может быть пустым");

            if (string.IsNullOrWhiteSpace(order))
                return BadRequest("Порядок сортировки не может быть пустым");

            var validSortBy = new[] { "Дата", "Название", "Статус", "Тип" };
            var validOrder = new[] { "Возрастание", "Убывание" };

            if (!validSortBy.Contains(sortBy))
                return BadRequest($"Неверное поле сортировки. Допустимые: {string.Join(", ", validSortBy)}");

            if (!validOrder.Contains(order))
                return BadRequest($"Неверный порядок сортировки. Допустимые: {string.Join(", ", validOrder)}");

            var publications = _context.Publications
                .Include(p => p.PublicationEdition)
                .AsQueryable();

            bool isAscending = order == "Возрастание";

            publications = sortBy switch
            {
                "Название" => isAscending ? publications.OrderBy(p => p.Title) : publications.OrderByDescending(p => p.Title),
                "Статус" => isAscending ? publications.OrderBy(p => p.Status) : publications.OrderByDescending(p => p.Status),
                "Тип" => isAscending
                    ? publications.OrderBy(p =>
                        _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Диссертация" :
                        _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                        _context.Articles.Any(a => a.PublicationId == p.Id) ? "Монография" :
                        _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" :
                        _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Статья" :
                        _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id) ? "Учебное пособие" : "Неизвестно")
                    : publications.OrderByDescending(p =>
                        _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Диссертация" :
                        _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                        _context.Articles.Any(a => a.PublicationId == p.Id) ? "Монография" :
                        _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" :
                        _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Статья" :
                        _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id) ? "Учебное пособие" : "Неизвестно"),
                _ => isAscending ? publications.OrderBy(p => p.PublicationDate) : publications.OrderByDescending(p => p.PublicationDate)
            };

            var result = publications.Select(p => new
            {
                p.Id,
                p.Title,
                p.Description,
                p.PublicationDate,
                p.Status,
                p.CreatedAt,
                EditionName = p.PublicationEdition != null ? p.PublicationEdition.EditionName : null,
                RINC = _context.ScientificPublications
                    .Where(sp => sp.PublicationId == p.Id)
                    .Select(sp => sp.RINC)
                    .FirstOrDefault(),
                IsScientific = _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id),
                PublicationType = _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Монография" :
                                 _context.Articles.Any(a => a.PublicationId == p.Id) ? "Статья" :
                                 _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                                 _context.Dissertations.Any(d => d.PublicationId == p.Id) ? "Диссертация" :
                                 _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Учебное пособие" :
                                 _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" : "Неизвестно"
            }).ToList();

            return Ok(result);
        }

        [HttpGet("publications/byyear")]
        [Authorize]
        public IActionResult GetPublicationsByYear(int year)
        {
            var publications = _context.Publications
                .Include(p => p.PublicationEdition)
                .Where(p => p.PublicationDate.Year == year)
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    p.PublicationDate,
                    p.Status,
                    p.CreatedAt,
                    EditionName = p.PublicationEdition != null ? p.PublicationEdition.EditionName : null,
                    RINC = _context.ScientificPublications
                        .Where(sp => sp.PublicationId == p.Id)
                        .Select(sp => sp.RINC)
                        .FirstOrDefault(),
                    IsScientific = _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id),
                    PublicationType = _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Монография" :
                                     _context.Articles.Any(a => a.PublicationId == p.Id) ? "Статья" :
                                     _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                                     _context.Dissertations.Any(d => d.PublicationId == p.Id) ? "Диссертация" :
                                     _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Учебное пособие" :
                                     _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" : "Неизвестно"
                }).ToList();

            if (!publications.Any())
                return NotFound($"Публикации за {year} год не найдены");

            return Ok(publications);
        }

        [HttpGet("publications/export")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult ExportPublicationsToExcel(int? employeeId = null)
        {
            var query = _context.Publications
                .Include(p => p.PublicationEdition)
                .AsQueryable();

            if (employeeId.HasValue)
                query = query.Where(p => p.EmployeeId == employeeId.Value);

            var publications = query.Select(p => new
            {
                p.Title,
                p.Description,
                p.PublicationDate,
                p.Status,
                Edition = p.PublicationEdition != null ? p.PublicationEdition.EditionName : "",
                RINC = _context.ScientificPublications
                    .Where(sp => sp.PublicationId == p.Id)
                    .Select(sp => sp.RINC)
                    .FirstOrDefault(),
                IsScientific = _context.ScientificPublications.Any(sp => sp.PublicationId == p.Id) ? "Да" : "Нет",
                PublicationType = _context.Monographs.Any(m => m.PublicationId == p.Id) ? "Монография" :
                                 _context.Articles.Any(a => a.PublicationId == p.Id) ? "Статья" :
                                 _context.Conferences.Any(c => c.PublicationId == p.Id) ? "Конференция" :
                                 _context.Dissertations.Any(d => d.PublicationId == p.Id) ? "Диссертация" :
                                 _context.Textbooks.Any(t => t.PublicationId == p.Id) ? "Учебное пособие" :
                                 _context.Patents.Any(pt => pt.PublicationId == p.Id) ? "Патент" : "Неизвестно"
            }).ToList();

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Публикации");

                worksheet.Cell(1, 1).Value = "Название";
                worksheet.Cell(1, 2).Value = "Описание";
                worksheet.Cell(1, 3).Value = "Дата публикации";
                worksheet.Cell(1, 4).Value = "Статус";
                worksheet.Cell(1, 5).Value = "Издание";
                worksheet.Cell(1, 6).Value = "РИНЦ";
                worksheet.Cell(1, 7).Value = "Научная";
                worksheet.Cell(1, 8).Value = "Тип публикации";

                for (int i = 0; i < publications.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = publications[i].Title;
                    worksheet.Cell(i + 2, 2).Value = publications[i].Description;
                    worksheet.Cell(i + 2, 3).Value = publications[i].PublicationDate.ToString("dd.MM.yyyy");
                    worksheet.Cell(i + 2, 4).Value = publications[i].Status;
                    worksheet.Cell(i + 2, 5).Value = publications[i].Edition;
                    worksheet.Cell(i + 2, 6).Value = publications[i].RINC ?? "";
                    worksheet.Cell(i + 2, 7).Value = publications[i].IsScientific;
                    worksheet.Cell(i + 2, 8).Value = publications[i].PublicationType;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "publications.xlsx");
                }
            }
        }
    }
}