﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using ICSharpCode.SharpDevelop.Dom;
using Rhino.Mocks;

namespace PackageManagement.Tests.Helpers
{
	public class AttributeHelper
	{
		public IAttribute Attribute;
		public IReturnType AttributeType;
		public List<object> PositionalArguments = new List<object>();
		public Dictionary<string, object> NamedArguments = new Dictionary<string, object>();
		
		public void CreateAttribute(string fullName)
		{
			CreateAttribute(fullName, fullName);
		}
		
		public void CreateAttribute(string fullName, string shortName)
		{
			AttributeType = MockRepository.GenerateStub<IReturnType>();
			AttributeType.Stub(at => at.FullyQualifiedName).Return(fullName);
			AttributeType.Stub(at => at.Name).Return(shortName);
			Attribute = MockRepository.GenerateStub<IAttribute>();
			Attribute.Stub(a => a.AttributeType).Return(AttributeType);
			Attribute.Stub(a => a.PositionalArguments).Return(PositionalArguments);
			Attribute.Stub(a => a.NamedArguments).Return(NamedArguments);
		}
		
		public void AddPositionalArguments(params object[] args)
		{
			PositionalArguments.AddRange(args);
		}
		
		public void AddNamedArgument(string name, object value)
		{
			NamedArguments.Add(name, value);
		}
		
		public void AddAttributeToClass(IClass @class)
		{
			var attributes = new List<IAttribute>();
			attributes.Add(Attribute);
			@class.Stub(c => c.Attributes).Return(attributes);
		}
	}
}
