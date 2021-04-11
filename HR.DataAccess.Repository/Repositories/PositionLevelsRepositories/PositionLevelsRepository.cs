using HR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HR.DataAccess.Repository.Repositories.PositionLevelsRepositories
{
    public class PositionLevelsRepository : IPositionLevelsRepository
    {
        private readonly ApplicationDbContext _context;
        public string msg;

        public PositionLevelsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(PositionLevel positionLevel)
        {
            try
            {
                if (positionLevel != null)
                {
                    _context.Add(positionLevel);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position Level doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            };
        }

        public void Delete(int PositionLevelId)
        {
            var positionLevel = _context.PositionLevels.Find(PositionLevelId);
            try
            {
                if (positionLevel != null)
                {
                    _context.PositionLevels.Remove(positionLevel);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position Level doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        public PositionLevel Get(int id)
        {
            var positionLevel = _context.PositionLevels.Find(id);

            if (positionLevel == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Position Level doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return positionLevel;

            }
        }

        public IEnumerable<PositionLevel> GetAll()
        {
            return _context.PositionLevels.ToList();
        }

        public void Update(int PositionLevelId, PositionLevel positionLevel)
        {
            try
            {
                if (positionLevel != null)
                {

                    _context.Entry(positionLevel).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position Level doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }
    }
}
