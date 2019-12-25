using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Permissions;

using SIO = System.IO;


// had my ass saved by
//	https://forum.unity.com/threads/missing-reference-callerlinenumber-in-the-namespace-system-runtime-compilerservices.533111/#post-4425331
namespace System.Runtime.CompilerServices
{
	using System.Security.Permissions;

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class CallerMemberNameAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class CallerFilePathAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class CallerLineNumberAttribute : Attribute
	{
	}
}

namespace Net35
{
	public static class AssSaver
	{
		public static T GetCustomAttribute<T>(Type type)
		{
			return GetCustomAttribute<T>(type, true);
		}

		public static T GetCustomAttribute<T>(Type type, bool inherited)
		{
			T r;
			Object[] ao = type.GetCustomAttributes(typeof(T), inherited);
			r = (0 == ao.Length) ? default : ((T)ao[0]);
			return r;
		}

		public static T GetCustomAttribute<T>(FieldInfo fieldInfo)
		{
			T r;
			Object[] ao = fieldInfo.GetCustomAttributes(typeof(T), true);
			r = (0 == ao.Length) ? default : ((T)ao[0]);
			return r;
		}

		public static T GetCustomAttribute<T>(PropertyInfo propertyInfo)
		{
			T r;
			Object[] ao = propertyInfo.GetCustomAttributes(typeof(T), true);
			r = (0 == ao.Length) ? default : ((T)ao[0]);
			return r;
		}

		public static T GetCustomAttribute<T>(MethodInfo methodInfo)
		{
			T r;
			Object[] ao = methodInfo.GetCustomAttributes(typeof(T), true);
			r = (0 == ao.Length) ? default : ((T)ao[0]);
			return r;
		}

		public static T GetCustomAttribute<T>(MemberInfo memberInfo)
		{
			T r;
			Object[] ao = memberInfo.GetCustomAttributes(typeof(T), true);
			r = (0 == ao.Length) ? default : ((T)ao[0]);
			return r;
		}

		public static Delegate GetDelegateFromMethodInfo(MethodInfo methodInfo)
		{
			var parameterInfos = methodInfo.GetParameters();
			Type[] argTypes = new Type[parameterInfos.Length+1];
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				argTypes[i] = parameterInfos[i].ParameterType;
			}

			if (typeof(void) != methodInfo.ReturnType)
				argTypes[parameterInfos.Length] = methodInfo.ReturnType;

#if net4
			Type delegateType = Expression.GetDelegateType(argTypes);
#else
			Type delegateType = (typeof(void) == (methodInfo.ReturnType)) ? Expression.GetActionType(argTypes) : Expression.GetFuncType(argTypes);
#endif
			return Delegate.CreateDelegate(delegateType, methodInfo);
		}
	}

	namespace System.IO
	{
		public static class Path
		{
			public static readonly char AltDirectorySeparatorChar	= SIO.Path.AltDirectorySeparatorChar;
			public static readonly char DirectorySeparatorChar		= SIO.Path.DirectorySeparatorChar;
			public static readonly char PathSeparator				= SIO.Path.PathSeparator;
			public static readonly char VolumeSeparatorChar			= SIO.Path.VolumeSeparatorChar;

			[Obsolete ("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
			public static readonly char[] InvalidPathChars			= SIO.Path.InvalidPathChars;

			public static string ChangeExtension(string path, string extension)		{ return SIO.Path.ChangeExtension(path, extension); }
			public static string Combine(string path1, string path2)				{ return SIO.Path.Combine(path1, path2); }
			public static string GetDirectoryName(string path)						{ return SIO.Path.GetDirectoryName(path); }
			public static string GetExtension(string path)							{ return SIO.Path.GetExtension(path); }
			public static string GetFileName(string path)							{ return SIO.Path.GetFileName(path); }
			public static string GetFileNameWithoutExtension(string path)			{ return SIO.Path.GetFileNameWithoutExtension(path); }
			public static string GetFullPath(string path)							{ return SIO.Path.GetFullPath(path); }
			public static char[] GetInvalidFileNameChars()							{ return SIO.Path.GetInvalidPathChars(); }
			public static char[] GetInvalidPathChars()								{ return SIO.Path.GetInvalidPathChars(); }
			public static string GetPathRoot(string path)							{ return SIO.Path.GetPathRoot(path); }
			public static string GetRandomFileName()								{ return SIO.Path.GetRandomFileName(); }

			[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
			public static string GetTempFileName()									{ return SIO.Path.GetTempFileName(); }

			[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
			public static string GetTempPath()										{ return SIO.Path.GetTempPath(); }

			public static bool HasExtension(string path)							{ return SIO.Path.HasExtension(path); }
			public static bool IsPathRooted(string path)							{ return SIO.Path.IsPathRooted(path); }

			public static string Combine(string path1, params string[] paths)
			{
				string r = path1;
				foreach (string p in paths)
					r = SIO.Path.Combine(r, p);
				return r;
			}
		}
	}
}
