// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Bookmarks;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.MbUnitPad
{
	/// <summary>
	/// Supplies a "Unit test" menu item if the class is a test fixture.
	/// </summary>
	public class MbUnitTestableCondition : IConditionEvaluator
	{
		public static IMember GetMember(object caller)
		{
			MemberNode memberNode = caller as MemberNode;
			if (memberNode != null) {
				return memberNode.Member;
			} else {
				ClassMemberBookmark mbookmark = caller as ClassMemberBookmark;
				if (mbookmark != null) {
					return mbookmark.Member;
				}
			}
			return null;
		}
		
		public static IClass GetClass(object caller)
		{
			ClassNode classNode = caller as ClassNode;
			if (classNode != null) {
				return classNode.Class;
			} else {
				ClassBookmark bookmark = caller as ClassBookmark;
				if (bookmark != null) {
					return bookmark.Class;
				}
			}
			return null;
		}
		
		public bool IsValid(object caller, Condition condition)
		{
			IMember m = GetMember(caller);
			IClass c = (m != null) ? m.DeclaringType : GetClass(caller);
			StringComparer nameComparer = c.ProjectContent.Language.NameComparer;
			string attributeName = (m != null) ? "Test" : "TestFixture";
			foreach (IAttribute attribute in (m ?? (IDecoration)c).Attributes) {
				if (nameComparer.Equals(attribute.Name, attributeName)
				    || nameComparer.Equals(attribute.Name, attributeName + "Attribute"))
				{
					return true;
				}
			}
			return false;
		}
	}
}
