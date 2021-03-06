﻿using System;
using System.Collections.Generic;
using System.Text;
using Tester.Core.Common;

namespace Tester.Dto.Users
{
    public class UserDto: BaseDto<Guid>
    {
        public string Login { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
