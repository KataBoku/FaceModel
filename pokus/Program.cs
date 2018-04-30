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
        static List<float>[] loadFaces(string directoryPath)
        {
            char[] DELIMITERS = { ' ', '\t' };
            const string VERTEX = "v";
            const string FACE = "f";

            var faceDictionarities = new DirectoryInfo(directoryPath).GetDirectories();

            //matrix of faces, vertexes of one face per one row
            List<float>[] faceMatrix = new List<float>[faceDictionarities.Length];

            //loads all face in face directory, 
            Parallel.For(0, faceDictionarities.Length, faceIndex =>
            {

                {
                    try
                    {
                        var faceDictionary = faceDictionarities.ElementAt(faceIndex);
                        StreamReader faceReader = new StreamReader(Path.Combine(directoryPath, faceDictionary.ToString(), "remesh.obj"));

                        faceMatrix[faceIndex] = new List<float>();
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
            List<float>[] faceMatrix = new List<float>[faceDictionarities.Length];

            //loads all face in face directory, 
            Parallel.For(0, faceDictionarities.Length, faceIndex =>
            {

                {
                    try
                    {
                        var faceDictionary = faceDictionarities.ElementAt(faceIndex);
                        StreamReader faceReader = new StreamReader(Path.Combine(directoryPath, faceDictionary.ToString(), "remesh.obj"));

                        faceMatrix[faceIndex] = new List<float>();
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
            List<float>[] checkedMatrix = checkLoadData(faceMatrix);
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

        private static List<float>[] checkLoadData(List<float>[] faceMatrix)
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
           // for (int rowIndex = 0; rowIndex < matrixSize; rowIndex++)

            Parallel.For(0, matrixSize, rowIndex =>
            {
                List<float> face = faceMatrix[rowIndex];

                //Parallel.For(0, matrixSize, colomnIndex =>
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


            Stopwatch stopwatch = Stopwatch.StartNew();

            List<float>[] faceMatrix = loadFaces(directoryPath);

            writeAndResetStopwatch(stopwatch, "load");


            float[] columnSums = getColumnSums(faceMatrix);
            writeAndResetStopwatch(stopwatch, "sum");

            means0(faceMatrix, columnSums);
            writeAndResetStopwatch(stopwatch, "mean");

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
            engine.Evaluate("options(max.print = 1)");
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
        static void runR(double[,] square, List<float>[] faceMatrix)
        {
            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
            engine.Evaluate("options(max.print = 5)");
            engine.Initialize();

             NumericMatrix group1 = engine.CreateNumericMatrix(square);

            //---------------------------------------------------------------------
            //NumericMatrix group1 = engine.CreateNumericMatrix(new double[,] {
            //        { 0, 0, 0, 0, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 0, 0, 1, 1, 1 },
            //        { 0, 0, 0, 1, 1 },
            //        { 1, 0, 0, 0, 0}
            //});



            engine.SetSymbol("group1", group1);
          
            GenericVector testResult = engine.Evaluate("(e<-eigen(group1))").AsList();


            var eigenValues = testResult["values"].AsNumeric().ToArray();
            var vectorOfVectors = testResult["vectors"].AsNumericMatrix().ToArray();

            //int indexNextEigenVector = 0;



            // Array.Sort(eigenValues,)

            //_allStatInfo.Sort(new Comparison<StatInfo>((x, y) => DateTime.Compare(x.date, y.date)));

            //double sum = eigenValues.Sum();
            //double[] kumulative = new double[eigenValues.Length];
            ////
            //bool firstOccurrence = false;
            //int thresholdIndex = -1;
            //double thresholdValue = 0.6;
            //for (int i = 0; i < eigenValues.Length; i++)
            //{
            //    double normalize = eigenValues[i] / sum;
            //    if (i == 0)
            //    {
            //        kumulative[i] = normalize;
            //    }
            //    else
            //    {
            //        kumulative[i] = normalize + kumulative[i - 1];
            //    }
            //    if (kumulative[i] >= thresholdValue && firstOccurrence == false)
            //    {
            //        firstOccurrence = true;
            //        thresholdIndex = i;
            //    }
            //}

            // engine.Evaluate("plot(group2, type = 'l', col = 'red', lwd = 10,ylab='eigen values')");





            //var normalizeEigenVaules = engine.CreateNumericVector(kumulative);


            //engine.SetSymbol("v", normalizeEigenVaules);
            // ylim = c(0, 1),type = 'b')
            engine.Evaluate("eigenSum<-sum(e$values)");
            //engine.Evaluate("cumsum(apply(as.matrix(e$values),1, function(x){(x-min(e$values))/(max(e$values)- min(e$values))}))");

            //zaporne hodnoty?????????
            engine.Evaluate("sink('C:/Users/Káťa/Documents/eigenValues.txt')");
      
            var cumulativeSum = engine.Evaluate("cumulativeSum <-cumsum(apply(as.matrix(e$values),2,function(x){x/eigenSum})");
            engine.Evaluate("sink()");
            engine.Evaluate("plot(cumsum(apply(as.matrix(e$values),2,function(x){x/eigenSum})), type = 'l', col = 'red', lwd = 10,ylab='eigen values')");

            int threshold = 1;
            int numberOfData = vectorOfVectors.GetLength(0);
            int faceDataLength = faceMatrix[0].Count();
            //each in row
            double[,] pcaVectors = new double[vectorOfVectors.GetLength(0), faceDataLength];
            Parallel.For(0, vectorOfVectors.GetLength(1), columnIndex =>
            {
                for (int k = 0; k < faceDataLength; k++)

                {
                    for (int rowIndex = 0; rowIndex < vectorOfVectors.GetLength(0); rowIndex++)
                    {
                        pcaVectors[columnIndex, k] += vectorOfVectors[rowIndex, columnIndex] * faceMatrix[rowIndex][k];
                    }

                }
            });

            double[,] values =  new double[ numberOfData, numberOfData];

            //X*pcaVectors
            Parallel.For(0, numberOfData, i =>
            {
                for (int j = 0; i < numberOfData; i++)
                {
                    for (int k = 0; k < faceDataLength; k++)
                    {
                        values[i, j] += faceMatrix[i][k] * pcaVectors[k, j];
                    }
                }

            });


                // return eigenValues;
            }

        /// <summary>
        /// Run PCA in R engine
        /// </summary>
        static void PCAR()
        {
            const string directoryPath = @"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes";
            Stopwatch stopwatch = Stopwatch.StartNew();
            var matrix = loadFacesR(directoryPath);
            writeAndResetStopwatch(stopwatch, "load");
     

            REngine.SetEnvironmentVariables();
            REngine engine = REngine.GetInstance();
           // engine.Evaluate("options(max.print = 1)");

            //var matrix = new double[,]  {{1,2,3,4},
            //    {0,2,2,-2},
            //   {3,4,5,6},
            //  {0,0,0,0}};

            NumericMatrix group1 = engine.CreateNumericMatrix(matrix);
            writeAndResetStopwatch(stopwatch, "save data");
            engine.SetSymbol("group1", group1);

            //---------------------------------porovnani nasobeni
            //engine.Evaluate("group1");
            //engine.Evaluate("group2 <- t(group1)");
            //writeAndResetStopwatch(stopwatch, "data");
            //engine.Evaluate("group3 <- group1 %*% group2");
            //writeAndResetStopwatch(stopwatch, "just Multiply");


            //GenericVector testResult = engine.Evaluate("pr.out=prcomp(group1,scale=TRUE)").AsList();
            //



            //writeAndResetStopwatch(stopwatch, "all PCA");

            var test = engine.Evaluate("pr.out=prcomp(group1,center=TRUE,scale=FALSE)").AsList();
            //var eigenVec = testResult["x"].AsNumericMatrix();
            engine.Evaluate("pr.out$sdev");
            engine.Evaluate("pr.var = pr.out$sdev ^ 2");
            engine.Evaluate("pr.var");
            engine.Evaluate("pve = pr.var / sum(pr.var)");
            engine.Evaluate("pve");
            engine.Evaluate("plot(cumsum(pve), xlab='Principal Component', ylab='Cumulative Proportion of Variance Explained', ylim=c(0,1),type='b')");
            // writeAndResetStopwatch(stopwatch, "oddelovatc");

       

            engine.Evaluate("sink('C:/Users/Káťa/Documents/pcaVectors.txt')");
            engine.Evaluate("pr.out$rotation");
            engine.Evaluate("sink()");
            engine.Evaluate("sink('C:/Users/Káťa/Documents/pcaValues.txt')");
            engine.Evaluate("cumsum(pve)");
            engine.Evaluate("sink()");

            //  engine.Evaluate("plot(cumsum(prr.var ), xlab='Principal Compo;nent', ylab='Cumulative Proportion of Variance Explained', ylim=c(0,1),type='b')");
            //  saveVector(eigenVec);

            // var eigenVec = testResult["pr.out&x"].AsNumericMatrix();





            Console.ReadLine();
        }
        /// <summary>
        /// Run multithread PCA
        /// </summary>
        static void PCA()
        {
            const string directoryPath = @"C:\Users\Káťa\Desktop\Diplomka\mesh\meshes";
            Stopwatch stopwatch = Stopwatch.StartNew();
            var dataMatrix = loadFaces(directoryPath);
            writeAndResetStopwatch(stopwatch, "load");

            if (dataMatrix == null)
            { return; }

            var columnSum =  getColumnSums(dataMatrix);
            writeAndResetStopwatch(stopwatch, "sum");

            means0(dataMatrix, columnSum);
            writeAndResetStopwatch(stopwatch, "mean");

            var multiplySmallMatrix = smallSquereMatrix(dataMatrix);
            writeAndResetStopwatch(stopwatch, "multiply");

            runR(multiplySmallMatrix,dataMatrix);
            writeAndResetStopwatch(stopwatch, "eigen...");

        

        }

        static void Main(string[] args)
        {
            //runR(null);
            PCAR();
            //PCA();


            Console.ReadLine();


        }
    }
}
