using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolderManager.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetNoteById
{
    public class GetNoteByIdQuery : IRequest<GetNoteByIdResponse>
    {
        public int Id { get; set; }
    }

    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, GetNoteByIdResponse> 
    {
        private readonly IFolderManagerDbContext _context;
        private readonly IMapper _mapper;
        public GetNoteByIdQueryHandler(IFolderManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetNoteByIdResponse> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Notes
                .Where(n => n.Id.Equals(request.Id))
                .ProjectTo<GetNoteByIdResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return response;
        }
    }
}
