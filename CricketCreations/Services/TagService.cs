using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CricketCreationsRepository.Repositories;
using CricketCreations.Interfaces;
using CricketCreations.Models;
using CricketCreationsRepository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CricketCreationsRepository.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CricketCreations.Services
{
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create(string json, int blogPostId, int userId)
        {
            NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<TagDTO>();
            ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(json);

            if (errors.Count == 0)
            {
                TagDTO tagDTO = JsonConvert.DeserializeObject<TagDTO>(json);
                TagDTO createdTagDTO = await _tagRepository.Create(tagDTO, blogPostId, userId);
                Tag tag = ConvertToTag(createdTagDTO);
                //return new ResponseBody<Tag>(tag, typeof(Tag).Name.ToString(), null);
                return new CreatedResult($"api/tag/{tag.Id}", tag);
            }
            else
            {
                List<ErrorObject> errs = new List<ErrorObject>();
                errors.ToList().ForEach(e =>
                {
                    errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                });
                return new BadRequestObjectResult(errs);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _tagRepository.Delete(id))
                {
                    return new StatusCodeResult(StatusCodes.Status204NoContent);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<IActionResult> Read(int id)
        {
            try
            {
                TagDTO tagDTO = await _tagRepository.Read(id);
                return new OkObjectResult(new ResponseBody<Tag>(ConvertToTag(tagDTO), typeof(Tag).Name.ToString(), null));
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Read(string page, string count)
        {
            try
            {
                List<Tag> tags;
                bool validPage = int.TryParse(page, out int pg);
                bool validCount = int.TryParse(count, out int cnt);
                int tagCount = await _tagRepository.GetCount();
                bool inRange = tagCount - (pg * cnt) >= ((cnt * -1) + 1);

                if (tagCount > 0 && !inRange)
                {
                    return new StatusCodeResult(StatusCodes.Status406NotAcceptable);
                }

                if (validPage && validCount)
                {
                    List<TagDTO> tagDTOs = await _tagRepository.Read(pg, cnt);
                    tags = tagDTOs.Select(b => ConvertToTag(b)).ToList();
                }
                else
                {
                    List<TagDTO> tagDTOs = await _tagRepository.Read();
                    tags = tagDTOs.Select(t => ConvertToTag(t)).ToList();
                }
                return new OkObjectResult(new OkObjectResult(new ResponseBody<List<Tag>>(tags, typeof(Tag).Name.ToString(), tagCount)));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IActionResult> Read(string page, string count, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Update(string jsonString)
        {
            try
            {
                NJsonSchema.JsonSchema jsonSchema = NJsonSchema.JsonSchema.FromType<TagDTO>();
                ICollection<NJsonSchema.Validation.ValidationError> errors = jsonSchema.Validate(jsonString);

                if (errors.Count == 0)
                {
                    TagDTO tagDTO = JsonConvert.DeserializeObject<TagDTO>(jsonString);
                    TagDTO updatedTagDTO = await _tagRepository.Update(tagDTO);
                    Tag tag = ConvertToTag(updatedTagDTO);
                    return new OkObjectResult(new ResponseBody<Tag>(tag, typeof(Tag).Name.ToString(), null));
                }
                else
                {
                    List<ErrorObject> errs = new List<ErrorObject>();
                    errors.ToList().ForEach(e =>
                    {
                        errs.Add(new ErrorObject() { Message = e.Kind.ToString(), Property = e.Property });
                    });
                    return new BadRequestObjectResult(errs);
                }
            }
            catch(Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        public TagDTO ConvertToTagDTO(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }
            return _mapper.Map<Tag, TagDTO>(tag);
        }
        public Tag ConvertToTag(TagDTO tagDTO)
        {
            if (tagDTO == null)
            {
                return null;
            }
            return _mapper.Map<TagDTO, Tag>(tagDTO);
        }
    }
}
