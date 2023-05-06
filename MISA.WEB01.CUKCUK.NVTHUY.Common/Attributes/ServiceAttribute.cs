﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB01.CUKCUK.NVTHUY.Common.Attributes
{
    /// <summary>
    /// Thuộc tính không cho phép null
    /// </summary>
    /// Created by: NVTHUY (20/04/2023)
    [AttributeUsage(AttributeTargets.Property)]
    public class NotAllowedNull : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính tên của property
    /// </summary>
    /// Created by: NVTHUY (20/04/2023)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropsName : Attribute
    {
        public string Name { get; set; }

        public PropsName(string name)
        {
            Name = name;
        }
    }

    /// <summary>
    /// Thuộc tính không cho phép trùng dữ liệu
    /// </summary>
    /// Created by: NVTHUY (20/04/2023)
    [AttributeUsage(AttributeTargets.Property)]
    public class NotAllowedDuplicate : Attribute
    {
    }

    /// <summary>
    /// Thuộc tính không phải là 1 parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotParameter : Attribute
    {
    }

}
