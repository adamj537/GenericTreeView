﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShaderEditor
{
  public interface INamedObjectItem : IObjectItem
  {
    string Name { get; set; }
  }
}
