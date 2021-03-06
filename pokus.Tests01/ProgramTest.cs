using System.Reflection;
using System.Collections.Generic;
// <copyright file="ProgramTest.cs">Copyright ©  2018</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pokus;

namespace pokus.Tests
{
    /// <summary>Tato třída obsahuje parametrizované testy jednotek pro Program.</summary>
    [PexClass(typeof(Program))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ProgramTest
    {


        [PexMethod]
        [PexMethodUnderTest("getColumnSums(List`1<Single>[])")]
        internal float[] getColumnSums(List<float>[] faceMatrix)
        {
            object[] args = new object[1];
            args[0] = (object)faceMatrix;
            Type[] parameterTypes = new Type[1];
            parameterTypes[0] = typeof(List<float>).MakeArrayType();
            float[] result0 = ((MethodBase)(typeof(Program).GetMethod("getColumnSums",
                                                                      BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic, (Binder)null,
                                                                      CallingConventions.Standard, parameterTypes, (ParameterModifier[])null)))
                                  .Invoke((object)null, args) as float[];
            float[] result = result0;
            return result;
            // TODO: přidat kontrolní výrazy do: metoda ProgramTest.getColumnSums(List`1<Single>[])
        }

    }
}
