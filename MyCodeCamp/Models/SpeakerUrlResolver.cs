﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MyCodeCamp.Data.Entities;
using MyCodeCamp.Helpers;

namespace MyCodeCamp.Models 
{
    public class SpeakerUrlResolver : IValueResolver<Speaker, SpeakerModel, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SpeakerUrlResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Resolve(
            Speaker source,
            SpeakerModel destination,
            string destMember,
            ResolutionContext context)
        {
            var url = (IUrlHelper)_httpContextAccessor.HttpContext.Items[Constants.UrlHelper];
            return url.Link("SpeakerGet", new { campId = source.Camp.Id, speakerId = source.Id });
        }
    }
}