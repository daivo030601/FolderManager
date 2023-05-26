using AutoMapper;
using AutoMapper.QueryableExtensions;
using FolderManager.Application.Common.Interfaces;
using FolderManager.Application.Dtos.NoteDto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderManager.Application.Notes.Queries.GetSearchNotes
{
    public class GetSearchNotesQuery : IRequest<GetSearchNotesResponse>
    {
        public string Keyword { get; set; }
        public int FolderId { get; set; }   
    }

    public class GetSearchNotesHandler : IRequestHandler<GetSearchNotesQuery, GetSearchNotesResponse>
    {
        private readonly IFolderManagerDbContext _context;
        private readonly IMapper _mapper;

        public GetSearchNotesHandler(IFolderManagerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task<GetSearchNotesResponse> Handle(GetSearchNotesQuery request, CancellationToken cancellationToken)
        {
            GetSearchNotesResponse response = new GetSearchNotesResponse();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                response.ResponseData = await _context.Notes
                    .Where(n => n.FolderId.Equals(request.FolderId) && n.Title.Contains(request.Keyword) || n.Content.Contains(request.Keyword))
                    .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }

            return response;
        }
    }
}
