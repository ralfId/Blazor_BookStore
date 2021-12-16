using AutoMapper;
using Business.Repositories.IRepository;
using Common;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class BookOrderDetailsRepository : IBookOrderDetailsRepository
    {
        private readonly AppDBContext _db;
        private IMapper _mapper;

        public BookOrderDetailsRepository(AppDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BookOrderDetailsDto> CreateOrderAsync(BookOrderDetailsDto details)
        {
            try
            {
                var orderDetails =  _mapper.Map<BookOrderDetailsDto, BookOrderDetails>(details);

                orderDetails.Status = ConstantsCommon.PS_Pending;

                var newOrder = await _db.BookOrderDetails.AddAsync(orderDetails);
                var result =  await _db.SaveChangesAsync();

                if (result > 0)
                {
                    return _mapper.Map<BookOrderDetails, BookOrderDetailsDto>(newOrder.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<BookOrderDetailsDto> GetOrderDetailAsync(int bookOrderId)
        {
            try
            {
                var orderDetail = await _db.BookOrderDetails
                                           .Include(b => b.Book)
                                           .ThenInclude(i => i.BookImages)
                                           .FirstOrDefaultAsync(x => x.Id == bookOrderId);

                return _mapper.Map<BookOrderDetails, BookOrderDetailsDto>(orderDetail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<IEnumerable<BookOrderDetailsDto>> GetOrderListAsync()
        {
            try
            {
                //var orderLst = await _db.BookOrderDetails.Include(b => b.Book).ToListAsync();
                //return _mapper.Map<IEnumerable<BookOrderDetails>, IEnumerable<BookOrderDetailsDto>>(orderLst);


                var orderLst = _mapper.Map<IEnumerable<BookOrderDetails>, IEnumerable<BookOrderDetailsDto>>(_db.BookOrderDetails.Include(b => b.Book));
                return orderLst;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<BookOrderDetailsDto> MarkSuccessfulPaymentAsync(int Id)
        {
            var data = await _db.BookOrderDetails.FindAsync(Id);

            if (data == null) return null;

            if (!data.IsPaid)
            {
                data.IsPaid = true;
                data.Status = ConstantsCommon.PS_Paid;

                var orderUpdated = _db.BookOrderDetails.Update(data);
                await _db.SaveChangesAsync();

                return _mapper.Map<BookOrderDetails, BookOrderDetailsDto>(orderUpdated.Entity);
            }

            return new BookOrderDetailsDto();
        }

        public Task<bool> UpdateOrderAsync(int bookOrderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
