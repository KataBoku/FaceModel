using System.Reflection;
using System.Collections.Generic;
// <copyright file="ProgramTest.cs">Copyright ©  2018</copyright>
using System.Linq;
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
        [PexAllowedException(typeof(TargetInvocationException))]

        // [PexAllowedExceptionFromTypeUnderTest(typeof(NullReferenceException))]
        // [PexAllowedException(typeof(IndexOutOfRangeException))]
        internal float[] getColumnSums(List<float>[] faceMatrix)
        {
            //tested during loading
            //null         
            PexAssume.IsNotNull(faceMatrix);
            //{}
            PexAssume.IsTrue(faceMatrix.Length != 0);
            //{null}
            PexAssume.IsNotNull(faceMatrix[0]);



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



        [PexMethod]
        [PexMethodUnderTest("means0(List`1<Single>[], Single[])")]
        internal void means0(List<float>[] faceMatrix, float[] columnMeans)
        {
            //tested during loading     
            //faceMatrix
            //null     
            PexAssume.IsNotNull(faceMatrix);
            //{}
            PexAssume.IsTrue(faceMatrix.Length != 0);
            //{null}
            PexAssume.IsNotNull(faceMatrix[0]);
            //rectangle matrix
            int count = faceMatrix[0].Count;
            PexAssume.IsTrue(faceMatrix.All(x => x != null && count == x.Count));

            //columnMeans
            PexAssume.IsNotNull(columnMeans);

            //same count of columns
            PexAssume.IsTrue(faceMatrix[0].Count == columnMeans.Length);


            object[] args = new object[2];
            args[0] = (object)faceMatrix;
            args[1] = (object)columnMeans;
            Type[] parameterTypes = new Type[2];
            parameterTypes[0] = typeof(List<float>).MakeArrayType();
            parameterTypes[1] = typeof(float).MakeArrayType();
            object result = ((MethodBase)(typeof(Program).GetMethod("means0",
                                                                    BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic, (Binder)null,
                                                                    CallingConventions.Standard, parameterTypes, (ParameterModifier[])null)))
                                .Invoke((object)null, args);
            // TODO: přidat kontrolní výrazy do: metoda ProgramTest.means0(List`1<Single>[], Single[])
        }



        [PexMethod]
        [PexMethodUnderTest("smallSquereMatrix(List`1<Single>[])")]
        internal double[,] smallSquereMatrix(List<float>[] faceMatrix)
        {
            //tested during loading
            //null         
            PexAssume.IsNotNull(faceMatrix);
            //{}
            PexAssume.IsTrue(faceMatrix.Length != 0);
            //{null}
            PexAssume.IsNotNull(faceMatrix[0]);
            //rectangle matrix
            int count = faceMatrix[0].Count;
            PexAssume.IsTrue(faceMatrix.All(x => x != null && count == x.Count));


            object[] args = new object[1];
            args[0] = (object)faceMatrix;
            Type[] parameterTypes = new Type[1];
            parameterTypes[0] = typeof(List<float>).MakeArrayType();
            double[,] result0 = ((MethodBase)(typeof(Program).GetMethod("smallSquereMatrix",
                                                                        BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic, (Binder)null,
                                                                        CallingConventions.Standard, parameterTypes, (ParameterModifier[])null)))
                                    .Invoke((object)null, args) as double[,];
            double[,] result = result0;
            return result;
            // TODO: přidat kontrolní výrazy do: metoda ProgramTest.smallSquertMatrix(List`1<Single>[])
        }

        [PexMethod]
        [PexMethodUnderTest("checkLoadData(List`1<Single>[])")]
        internal List<float>[] checkLoadData(List<float>[] faceMatrix)
        {
            object[] args = new object[1];
            args[0] = (object)faceMatrix;
            Type[] parameterTypes = new Type[1];
            parameterTypes[0] = typeof(List<float>).MakeArrayType();
            List<float>[] result0 = ((MethodBase)(typeof(Program).GetMethod("checkLoadData",
                                                                            BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic, (Binder)null,
                                                                            CallingConventions.Standard, parameterTypes, (ParameterModifier[])null)))
                                        .Invoke((object)null, args) as List<float>[];
            List<float>[] result = result0;
            return result;
            // TODO: přidat kontrolní výrazy do: metoda ProgramTest.checkLoadData(List`1<Single>[])
        }

    
    }
}
