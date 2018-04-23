using RDotNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace pokus
{
    class Program

    {
        static void saveVector(NumericMatrix eigenVectors)
        {
            char[] DELIMITERS = { ' ', '\t' };
            const string VERTEX = "v";
            const string FACE = "f";
            try
            {

                StreamReader faceReader = new StreamReader(Path.Combine(@"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes", "0", "remesh.obj"));
                StreamWriter faceWriter = new StreamWriter(Path.Combine(@"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes", "0", "newRemesh.obj"));

                int i = 0;
                String line;
                //gets only vertex data from the face file
                while ((line = faceReader.ReadLine()) != null)
                {

                    string[] tokens = line.Split(DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 0)
                        continue;
                    if (tokens[0] == VERTEX)
                    {
                        //TODO aktualni data
                        faceWriter.WriteLine(VERTEX + "\t" + string.Join("\t", new float[] { (float)eigenVectors[1, i++], (float)eigenVectors[1, i++], (float)eigenVectors[1, i++] }));


                    }

                    ///TODO muze byt jine poradi????, takhle je to rychlejsi o 2 sekundy
                    if (tokens[0] == FACE)
                        faceWriter.WriteLine(line);

                }

                faceReader.Close();
                faceWriter.Close();
            }
            catch (IOException)
            {
                //TODO show messagebox
                throw new Exception("chyba nacitani ci ukladani");
            }
        }
        static void saveApproximation(string directoryPath)
        {
            char[] DELIMITERS = { ' ', '\t' };
            const string VERTEX = "v";
            const string FACE = "f";

            var faceDictionarities = new DirectoryInfo(directoryPath).GetDirectories();


            //saves new approximat face
            //TODO vsechnz<<<::/..,,
            Parallel.For(0, 2, faceIndex =>
            {

                {
                    try
                    {
                        var faceDictionary = faceDictionarities.ElementAt(faceIndex);
                        StreamReader faceReader = new StreamReader(Path.Combine(directoryPath, faceDictionary.ToString(), "remesh.obj"));
                        StreamWriter faceWriter = new StreamWriter(Path.Combine(directoryPath, faceDictionary.ToString(), "newRemesh.obj"));


                        String line;
                        //gets only vertex data from the face file
                        while ((line = faceReader.ReadLine()) != null)
                        {

                            string[] tokens = line.Split(DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length == 0)
                                continue;
                            if (tokens[0] == VERTEX)
                            {
                                //TODO aktualni data
                                faceWriter.WriteLine(VERTEX + "\t" + string.Join("\t", new float[] { 1.5f, 2, 2 }));


                            }

                            ///TODO muze byt jine poradi????, takhle je to rychlejsi o 2 sekundy
                            if (tokens[0] == FACE)
                                faceWriter.WriteLine(line);

                        }

                        faceReader.Close();
                        faceWriter.Close();
                    }
                    catch (IOException)
                    {
                        //TODO show messagebox
                        throw new Exception("chyba nacitani ci ukladani");
                    }
                }


            });
        }
        static List<double>[] loadFaces(string directoryPath)
        {
            char[] DELIMITERS = { ' ', '\t' };
            const string VERTEX = "v";
            const string FACE = "f";

            var faceDictionarities = new DirectoryInfo(directoryPath).GetDirectories();

            //matrix of faces, vertexes of one face per one row
            List<double>[] faceMatrix = new List<double>[faceDictionarities.Length];

            //loads all face in face directory, 
            Parallel.For(0, faceDictionarities.Length, faceIndex =>
            {

                {
                    try
                    {
                        var faceDictionary = faceDictionarities.ElementAt(faceIndex);
                        StreamReader faceReader = new StreamReader(Path.Combine(directoryPath, faceDictionary.ToString(), "remesh.obj"));

                        faceMatrix[faceIndex] = new List<double>();
                        float x, y, z;

                        String line;
                        //gets only vertex data from the face file
                        while ((line = faceReader.ReadLine()) != null)
                        {

                            string[] tokens = line.Split(DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length == 0)
                                continue;
                            if (tokens[0] == VERTEX)
                            {
                                if (!float.TryParse(tokens[1], NumberStyles.Float, CultureInfo.InvariantCulture, out x) ||
                                    !float.TryParse(tokens[2], NumberStyles.Float, CultureInfo.InvariantCulture, out y) ||
                                    !float.TryParse(tokens[3], NumberStyles.Float, CultureInfo.InvariantCulture, out z))
                                    continue;

                                faceMatrix[faceIndex].Add(x);
                                faceMatrix[faceIndex].Add(y);
                                faceMatrix[faceIndex].Add(z);

                            }

                            ///TODO muze byt jine poradi????, takhle je to rychlejsi o 2 sekundy
                            if (tokens[0] == FACE)
                                break;

                        }

                        faceReader.Close();
                    }
                    catch (IOException)
                    {
                        //TODO show messagebox
                        throw new Exception("chyba nacitani");
                    }
                }

            });
            return checkLoadData(faceMatrix);

        }

        /// <summary>
        /// Loads vertices of faces from file.obj to the matrix
        /// </summary>
        /// <param name="directoryPath">Path to directory with faces</param>
        /// <returns> Returns matrix, where one row of matrix contains all vertex coordinations of conreate face</returns>
        static double[,] loadFacesR(string directoryPath)
        {
            char[] DELIMITERS = { ' ', '\t' };
            const string VERTEX = "v";
            const string FACE = "f";

            var faceDictionarities = new DirectoryInfo(directoryPath).GetDirectories();

            //matrix of faces, vertexes of one face per one row
            List<double>[] faceMatrix = new List<double>[faceDictionarities.Length];

            //loads all face in face directory, 
            Parallel.For(0, faceDictionarities.Length, faceIndex =>
            {

                {
                    try
                    {
                        var faceDictionary = faceDictionarities.ElementAt(faceIndex);
                        StreamReader faceReader = new StreamReader(Path.Combine(directoryPath, faceDictionary.ToString(), "remesh.obj"));

                        faceMatrix[faceIndex] = new List<double>();
                        float x, y, z;

                        String line;
                        //gets only vertex data from the face file
                        while ((line = faceReader.ReadLine()) != null)
                        {

                            string[] tokens = line.Split(DELIMITERS, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length == 0)
                                continue;
                            if (tokens[0] == VERTEX)
                            {
                                if (!float.TryParse(tokens[1], NumberStyles.Float, CultureInfo.InvariantCulture, out x) ||
                                    !float.TryParse(tokens[2], NumberStyles.Float, CultureInfo.InvariantCulture, out y) ||
                                    !float.TryParse(tokens[3], NumberStyles.Float, CultureInfo.InvariantCulture, out z))
                                    continue;

                                faceMatrix[faceIndex].Add(x);
                                faceMatrix[faceIndex].Add(y);
                                faceMatrix[faceIndex].Add(z);

                            }

                            ///TODO muze byt jine poradi????, takhle je to rychlejsi o 2 sekundy
                            if (tokens[0] == FACE)
                                break;

                        }

                        faceReader.Close();
                    }
                    catch (IOException)
                    {
                        //TODO show messagebox
                        throw new Exception("chyba nacitani");
                    }
                }

            });

            // return checkLoadData(faceMatrix);
            List<double>[] checkedMatrix = checkLoadData(faceMatrix);
            double[,] rResult = new double[checkedMatrix.Length, checkedMatrix[0].Count];
            if (checkedMatrix != null)
            {
                for (int i = 0; i < checkedMatrix.Length; i++)
                {
                    var k = checkedMatrix[i];
                    for (int j = 0; j < k.Count; j++)
                    {
                        rResult[i, j] = k[j];
                    }
                }

            }

            return rResult;




        }

        private static List<double>[] checkLoadData(List<double>[] faceMatrix)
        {
            if (faceMatrix == null || faceMatrix.Length == 0 || faceMatrix[0] == null || faceMatrix[0].Count == 0)
            {
                //TODO show failer
                //throw new Exception("chyba zadna data");
                return null;

            }
            int vertexNumber = faceMatrix[0].Count;


            foreach (var face in faceMatrix)
            {
                //TODO show failer
                if (face == null)
                {
                    //throw new Exception("chyba null face");
                    return null;
                }
                //TODO show failer
                if (vertexNumber != face.Count)
                {
                    //throw new Exception("nesedi pocet vrcholu");
                    return null;
                }

            }
            return faceMatrix;
        }

        /// <summary>
        /// Shows the duration of the function.
        /// </summary>
        /// <param name="stopwatch">Stopwatch object</param>
        /// <param name="title">Console title</param>
        static void writeAndResetStopwatch(Stopwatch stopwatch, string title)
        {
            stopwatch.Stop();
            Console.WriteLine("Data {1} {0} ms ", stopwatch.ElapsedMilliseconds, title);
            stopwatch.Reset();
            stopwatch.Start();
        }
        /// <summary>
        /// Sums columns values;
        /// </summary>
        /// <param name="faceMatrix">Matrix with face data</param>
        /// <returns>Retuns arry with the sum of columns</returns>
        static float[] getColumnSums(List<float>[] faceMatrix)
        {


            int numberOfVertexies = faceMatrix[0].Count;
            float[] columnMeans = new float[numberOfVertexies];

            //gets sum of in each face
            Parallel.For(0, faceMatrix.Length, faceIndex =>
            {
                List<float> face = faceMatrix[faceIndex];


                for (int vertexIndex = 0; vertexIndex < columnMeans.Length; vertexIndex++)
                {
                    columnMeans[vertexIndex] += face[vertexIndex];
                }
            });
            return columnMeans;
        }
        /// <summary>
        /// Subtract colums mean from each value this column 
        /// </summary>
        /// <param name="faceMatrix">Matrix with face data</param>
        /// <param name="columnMeans">Array of column means</param>
        static void means0(List<float>[] faceMatrix, float[] columnMeans)
        {

            Parallel.For(0, faceMatrix.Length, faceIndex =>
            {
                List<float> face = faceMatrix[faceIndex];

                for (int vertexIndex = 0; vertexIndex < columnMeans.Length; vertexIndex++)
                {
                    face[vertexIndex] -= columnMeans[vertexIndex] / faceMatrix.Length;
                }
            });

        }
        /// <summary>
        /// Multiplies matrix with transpose matrix to get small square matrix.
        /// </summary>
        /// <param name="faceMatrix">Input face matrix</param>
        /// <returns></returns>
        static double[,] smallSquereMatrix(List<float>[] faceMatrix)
        {
            int matrixSize = faceMatrix.Length;
            double[,] XtimesX = new double[matrixSize, matrixSize];
            Parallel.For(0, matrixSize, rowIndex =>
            {
                List<float> face = faceMatrix[rowIndex];

                for (int colomnIndex = 0; colomnIndex < matrixSize; colomnIndex++)
                {
                    for (int k = 0; k < face.Count; k++)
                    {

                        XtimesX[rowIndex, colomnIndex] += faceMatrix[rowIndex][k] * faceMatrix[colomnIndex][k];
                    }
                }

            });
            return XtimesX;
        }
        /// <summary>
        /// Gets eigenvalues for model. 
        /// </summary>
        /// <param name="directoryPath"></param>
        static void getDataMatrix(string directoryPath)
        {


            //Stopwatch stopwatch = Stopwatch.StartNew();

            //List<float>[] faceMatrix = loadFaces(directoryPath);

            //writeAndResetStopwatch(stopwatch, "load");


            //float[] columnSums = getColumnSums(faceMatrix);
            //writeAndResetStopwatch(stopwatch, "sum");

            //means0(faceMatrix, columnSums);
            //writeAndResetStopwatch(stopwatch, "mean");






            Console.ReadLine();

        }
        static void pokusR2()
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();

        }
        static void pokus()
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            // REngine requires explicit initialization.
            // You can set some parameters.
            engine.Initialize();
            NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();
            //engine.Evaluate("plot(group2)");
            engine.Evaluate(" getwd()");
            engine.Evaluate("file.path('C:', 'Users', 'Káťa','Desktop','Diplomka' ,'rplot.jpg')");
            engine.Evaluate("jpeg(file = file.path('C:', 'Users', 'Káťa','Desktop','Diplomka' ,'rplot.jpg'))");
            engine.Evaluate("plot(group2, type = 'l', col = 'red', lwd = 10,ylab='eigen values')");
            engine.Evaluate("dev.off()");
            engine.Dispose();
        }
        static void runR(double[,] square)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            // REngine requires explicit initialization.
            // You can set some parameters.
            engine.Initialize();
            NumericMatrix group1 = engine.CreateNumericMatrix(square);
            //NumericMatrix group1 = engine.CreateNumericMatrix(new double[,] {
            //        { 0, 0, 0, 0, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 0, 0, 1, 1, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 0, 0, 0, 0, 1 }
            //});
            engine.SetSymbol("group1", group1);
            GenericVector testResult = engine.Evaluate("eigen(group1)").AsList();
            Console.WriteLine(testResult);
            var hokus = testResult["values"].AsIntegerMatrix();
            var eigenValues = testResult["values"].AsNumeric().ToArray();
            var vectorOfVectors = testResult["vectors"].AsNumeric();

            int indexNextEigenVector = 0;

            // double[] eigenVectors 
            //for (int i = 0; i < eigenVectors.Length; i++)
            //{
            //    if ()
            //}
            //Array.Sort(eigenValues, eigenVectors);
            //_allStatInfo.Sort(new Comparison<StatInfo>((x, y) => DateTime.Compare(x.date, y.date)));

            double sum = eigenValues.Sum();
            double[] kumulative = new double[eigenValues.Length];
            //
            bool firstOccurrence = false;
            int thresholdIndex = -1;
            double thresholdValue = 0.6;
            for (int i = 0; i < eigenValues.Length; i++)
            {
                double normalize = eigenValues[i] / sum;
                if (i == 0)
                {
                    kumulative[i] = normalize;
                }
                else
                {
                    kumulative[i] = normalize + kumulative[i - 1];
                }
                if (kumulative[i] >= thresholdValue && firstOccurrence == false)
                {
                    firstOccurrence = true;
                    thresholdIndex = i;
                }
            }

            // engine.Evaluate("plot(group2, type = 'l', col = 'red', lwd = 10,ylab='eigen values')");





            var normalizeEigenVaules = engine.CreateNumericVector(kumulative);


            engine.SetSymbol("v", normalizeEigenVaules);
            // ylim = c(0, 1),type = 'b')
            engine.Evaluate("plot(v, type = 'l', col = 'red', lwd = 10,ylab='eigen values',type = 'b')");


            var hokz = testResult["values"].AsList().ToArray();
            Console.WriteLine("P-value = {0}", hokus[1, 0]);

            // return eigenValues;
        }

        /// <summary>
        /// Run PCA in R engine
        /// </summary>
        static void PCAR()
        {
            const string directoryPath = @"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes";
            var matrix = loadFacesR(directoryPath);
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            NumericMatrix group1 = engine.CreateNumericMatrix(matrix);

            engine.SetSymbol("group1", group1);
            GenericVector testResult = engine.Evaluate("pr.out=prcomp(group1, scale=TRUE)").AsList();
            var eigenVec = testResult["x"].AsNumericMatrix();
            engine.Evaluate("pr.out$sdev");
            engine.Evaluate("pr.var = pr.out$sdev ^ 2");
            engine.Evaluate("pr.var");
            engine.Evaluate("pve = pr.var / sum(pr.var)");
            engine.Evaluate("pve");
            engine.Evaluate("plot(cumsum(pve), xlab='Principal Component', ylab='Cumulative Proportion of Variance Explained', ylim=c(0,1),type='b')");
            saveVector(eigenVec);

            // var eigenVec = testResult["pr.out&x"].AsNumericMatrix();

            Console.ReadLine();
        }
        /// <summary>
        /// Run multithread PCA
        /// </summary>
        static void PCA()
        {
            const string directoryPath = @"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes";
            var matrix = loadFacesR(directoryPath);
        }

        static void Main(string[] args)
        {
            const string directoryPath = @"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes";
            var matrix = loadFacesR(directoryPath);
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            NumericMatrix group1 = engine.CreateNumericMatrix(matrix);

            //List<float>[] faceMatrix = new List<float>[] { };
            //double[,] squere = new double[,] { { 13,  -4,    2 },
            //  {-4 ,  11  , -2},  {2 ,  -2  ,  8 } };
            //runR(squere);
            //int[] i = new int[] {};
            // pokus();


            // var matrix = getDataMatrix(directoryPath);

            // runR();


            //REngine.SetEnvironmentVariables();
            //REngine engine = REngine.GetInstance();
            //// REngine requires explicit initialization.
            //// You can set some parameters.
            //engine.Initialize();
            //NumericMatrix group1 = engine.CreateNumericMatrix(new double[,] {
            //        { 0, 0, 0, 0, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 0, 0, 1, 1, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 0, 0, 0, 0, 1 }
            //});
            //engine.SetSymbol("group1", group1);
            //GenericVector testResult = engine.Evaluate("eigen(group1)").AsList();
            //Console.WriteLine("P-value = {0}", group1);




            // //.NET Framework array to R vector.
            //NumericVector group1 = engine.CreateNumericVector(new double[] { 30.02, 29.99, 30.11, 29.97, 30.01, 29.99 });
            //engine.SetSymbol("group1", group1);
            //// Direct parsing from R script.
            //NumericVector group2 = engine.Evaluate("group2 <- c(29.89, 29.93, 29.72, 29.98, 30.02, 29.98)").AsNumeric();


            //// Test difference of mean and get the P-value.
            //GenericVector testResult = engine.Evaluate("t.test(group1, group2)").AsList();
            //double p = testResult["p.value"].AsNumeric().First();

            //Console.WriteLine("Group1: [{0}]", string.Join(", ", group1));
            //Console.WriteLine("Group2: [{0}]", string.Join(", ", group2));
            //Console.WriteLine("P-value = {0:0.000}", p);

            //you should always dispose of the REngine properly.
            // After disposing of the engine, you cannot reinitialize nor reuse it
            // engine.Dispose();
            Console.ReadLine();


        }
    }
}
