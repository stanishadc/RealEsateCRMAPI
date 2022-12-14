using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEsateCRMAPI.DTO;
using RealEsateCRMAPI.Entities;
using System.Net;

namespace RealEsateCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly DBContext DBContext;

        public DesignationController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<DesignationDTO>>> Get()
        {
            var List = await DBContext.Designations.Select(
                s => new DesignationDTO
                {
                    DesignationId = s.DesignationId,
                    Name = s.Name,
                    Status = s.Status,
                    Commission = s.Commission
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Designation>> GetById(int Id)
        {
            var designation = await DBContext.Designations.Where(s => s.DesignationId == Id).FirstOrDefaultAsync();
            if (designation == null)
            {
                return NotFound();
            }
            else
            {
                return designation;
            }
        }
        [HttpPost]
        public async Task<HttpStatusCode> Insert(DesignationDTO designationDTO)
        {
            var entity = new Designation()
            {
                Name = designationDTO.Name,
                Status = designationDTO.Status,
                Commission = designationDTO.Commission,
                ParentId = designationDTO.ParentId
            };
            DBContext.Designations.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
        [HttpPut]
        public async Task<HttpStatusCode> Update(DesignationDTO designationDTO)
        {
            var entity = await DBContext.Designations.FirstOrDefaultAsync(s => s.DesignationId == designationDTO.DesignationId);
            entity.Name = designationDTO.Name;
            entity.Status = designationDTO.Status;
            entity.Commission = designationDTO.Commission;
            entity.ParentId = designationDTO.ParentId;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
        [HttpDelete("{Id}")]
        public async Task<HttpStatusCode> Delete(int Id)
        {
            var entity = new Designation()
            {
                DesignationId = Id
            };
            DBContext.Designations.Attach(entity);
            DBContext.Designations.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
