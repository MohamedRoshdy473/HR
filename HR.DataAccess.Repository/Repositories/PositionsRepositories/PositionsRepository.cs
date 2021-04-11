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

namespace HR.DataAccess.Repository.Repositories.PositionsRepositories
{
   public class PositionsRepository:IPositionsRepository
    {
        protected readonly ApplicationDbContext _context;
        string msg;

        public PositionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Positions position)
        {
            try
            {
                if (position != null)
                {
                    _context.Add(position);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public void Delete(int positionId)
        {
            var position = _context.Positions.Find(positionId);
            try
            {
                if (position != null)
                {
                    _context.Positions.Remove(position);
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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

        public Positions Get(int id)
        {
            var positions =  _context.Positions.Find(id);

            if (positions == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Position doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            else
            {
                return positions;

            }
        }

        public IEnumerable<Positions> GetAll()
        {
            return _context.Positions.ToList();
        }
        public void Update(int positionId, Positions position)
        {
            try
            {
                if (position != null)
                {
                    _context.Entry(position).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                else
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Position doesn't exist", System.Text.Encoding.UTF8, "text/plain"),
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
