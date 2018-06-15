﻿using System.Collections.Generic;

namespace Shop.Api.Api.Model
{
    public abstract class ErrorResponse
    {
        public abstract string Code { get; }
        public abstract string Message { get; }

        public class NotFound : ErrorResponse
        {
            public override string Code => "Not Found";
            public override string Message => "The requested resource was not found";
        }

        public class Validation : ErrorResponse
        {
            public override string Code => "Bad Request";
            public override string Message => "One or more input validation is invalid";
            public List<ValidationItem> Errors { get; set; }

            public class ValidationItem
            {
                public string Key { get; set; }
                public string Message { get; set; }
            }
        }

        public class InternalServerError : ErrorResponse
        {
            public override string Code => "InternalServerError";
            public override string Message => "Oops! something went wrong!";
            public string Details { get; set; }
        }

        public class Conflict : ErrorResponse
        {
            public override string Code => "Conflict";
            public override string Message => "One or more business rules failed";
            public List<ConflictItem> Errors { get; set; }

            public class ConflictItem
            {
                public string Code { get; set; }
                public string Message { get; set; }
            }
        }
    }
}
