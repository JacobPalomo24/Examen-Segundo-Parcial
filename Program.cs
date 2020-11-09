/*
 * MIT License
 * Copyright (c) 2020 Jacob Palomo
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 */

using System;
using System.IO;

namespace Exam2ndPartial
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathClientes = @"C:\Users\jocel\source\repos\Exam2ndPartial\clientes.txt";
            string pathProductos = @"C:\Users\jocel\source\repos\Exam2ndPartial\productos.txt";
            init(pathClientes, pathProductos);

        }

        public static void init(string pathClientes, string pathProductos)
        {
            Console.WriteLine("¿Qué deseas hacer?\n");

            Console.WriteLine("C1. Dar de alta un cliente            | C2. Modificar un cliente                  | C3. Dar de baja un cliente");
            Console.WriteLine("C4. Consultar clientes existentes     | C5. Mostrar todos los nombres registrados \n");
            Console.WriteLine("P1. Dar de alta un producto           | P2. Modificar un producto                 | P3. Eliminar un producto");
            Console.WriteLine("P4. Productos con poca disponibilidad | P5. Ver todos los productos activos");

            Console.WriteLine("\nPara salir escribe 'S' y presiona enter.");
            Console.Write("\nOpción: ");
            string op = Console.ReadLine().ToUpper();

            switch (op)
            {
                case "C1":
                    {
                        addClient(pathClientes);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "C2":
                    {
                        editClient(pathClientes);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "C3":
                    {
                        deleteClient(pathClientes);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "C4":
                    {
                        viewActiveClients(pathClientes);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "C5":
                    {
                        viewAllNameClients(pathClientes);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "P1":
                    {
                        addProduct(pathProductos);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "P2":
                    {
                        editProduct(pathProductos);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "P3":
                    {
                        deleteProduct(pathProductos);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "P4":
                    {
                        viewLowAvailabilityProducts(pathProductos);
                        init(pathClientes, pathProductos);
                        break;
                    }
                
                case "P5":
                    {
                        viewAllProducts(pathProductos);
                        init(pathClientes, pathProductos);
                        break;
                    }

                case "S":
                    {
                        break;
                    }

                default:
                    {
                        Console.Clear();
                        Console.Write("Esa opción no existe, ingresa una de la lista por favor.");
                        init(pathClientes, pathProductos);
                        break;
                    }
            }
        }

        private static void addClient(string path)
        {
            Console.Clear();

            Console.Write("Nombre completo: ");
            string name = Console.ReadLine().ToUpper();
            Console.Write("Direción: ");
            string address = Console.ReadLine().ToUpper();
            Console.Write("Código postal: ");
            string cp = Console.ReadLine().ToUpper();
            Console.Write("Teléfono: ");
            string phone = Console.ReadLine().ToUpper();
            Console.Write("Correo: ");
            string email = Console.ReadLine().ToUpper();

            Console.Clear();
            Console.WriteLine("¿La información es correcta?");
            Console.WriteLine("Nombre:        " + name);
            Console.WriteLine("Dirección:     " + address);
            Console.WriteLine("Código postal: " + cp);
            Console.WriteLine("Teléfono:      " + phone);
            Console.WriteLine("Correo:        " + email);

            Console.WriteLine("\ns: Si | n: No");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            if (op == "s")
            {
                bool clienteExistente = false;

                if (File.Exists(path))
                {
                    StreamReader lectura = File.OpenText(path);
                    string clientes = lectura.ReadToEnd();
                    string[] cliente = clientes.Split('\n');
                    lectura.Close();



                    for (int i = 1; i < cliente.Length; i += 8)
                    {
                        if (cliente[i].Substring(15).Equals(name))
                        {
                            clienteExistente = true;
                        }
                    }
                }

                if (!clienteExistente)
                {
                    string hr = "\n-----------------------------------------------------------------------------------------";
                    string newClientInfo = "NOMBRE:        " + name + "\n" +
                                           "DIRECCION:     " + address + "\n" +
                                           "CODIGO POSTAL: " + cp + "\n" +
                                           "TELEFONO:      " + phone + "\n" +
                                           "CORREO:        " + email + "\n" +
                                           "ESTATUS:       true";

                    int id = 1;

                    if (!File.Exists(path))
                    {
                        id = 1;
                        File.WriteAllText(path, "ID:            " + id + "\n" + newClientInfo);

                        Console.Clear();
                        Console.WriteLine("Archivo clientes.txt creado correctamente.");
                        Console.WriteLine("Cliente agregado correctamente.\n");
                    }
                    else
                    {

                        StreamReader lectura = File.OpenText(path);
                        string clientes = lectura.ReadToEnd();
                        string[] cliente = clientes.Split('\n');
                        lectura.Close();

                        for (int i = 0; i < cliente.Length; i += 8)
                        {
                            if (cliente[i].Substring(0, 2).Equals("ID"))
                            {
                                id = int.Parse(cliente[i].Substring(15));
                            }
                        }

                        id++;

                        Console.Clear();
                        File.AppendAllText(path, hr + "\nID:            " + id + "\n" + newClientInfo);
                        Console.WriteLine("Cliente agregado correctamente.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("El cliente ya existe, elimínelo, o modifíquelo por favor.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            } 
            else if(op == "n")
            {
                Console.Clear();
                addClient(path);
            }
            else
            {
                Console.WriteLine("Esa aopción no está disponible, se tomará como un NO.");
                Console.ReadKey();
                Console.Clear();
                addClient(path);
            }
        }

        private static void editClient(string path)
        {
            Console.Clear();

            if (!File.Exists(path))
            {
                Console.Clear();
                Console.Write("Aún no hay nada que modificar :(");
                Console.ReadKey(true);
            }
            else
            {
                StreamReader lectura = File.OpenText(path);
                string clientes = lectura.ReadToEnd();
                string[] cliente = clientes.Split('\n');
                lectura.Close();

                Console.Write("Ingresa el nombre del cliente: ");
                string sName = Console.ReadLine().ToUpper();

                bool encontrado = false;
                int pos = 0;

                for (int i = 1; i < cliente.Length; i += 8)
                {
                    if (cliente[i].Substring(0, 6).Equals("NOMBRE"))
                    {
                        if (cliente[i].Substring(15).Equals(sName))
                        {
                            encontrado = true;
                            pos = i;
                            break;
                        }
                        else
                        {
                            encontrado = false;
                        }
                    }
                }

                if (encontrado)
                {
                    Console.Clear();

                    if (bool.Parse(cliente[pos+5].Substring(15)))
                    {
                        // Muestra el cliente a modificar
                        for (int i = 0; i < 5; i++)
                        {
                            Console.WriteLine(cliente[pos]);
                            pos++;
                        }
                        Console.WriteLine("\n¿Qué deseas editar? (Escribe el nombre del atributo, por ejemplo: 'Nombre')");
                        Console.WriteLine("Para regresar solo presiona enter sin escribir nada.");
                        Console.Write("Atributo: ");
                        string attr = Console.ReadLine().ToUpper();

                        pos -= 5;
                        switch (attr)
                        {
                            case "NOMBRE":
                                {
                                    Console.Write("\n¿Cuál es el nuevo nombre del cliente?: ");
                                    string newName = Console.ReadLine().ToUpper();
                                    
                                    // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                    for (int i = pos; i < pos+5; i++)
                                    {
                                        if (cliente[i].Substring(0, 6).Equals("NOMBRE"))
                                        {
                                            break;
                                        }
                                    }

                                    cliente[pos] = "NOMBRE:        " + newName;

                                    StreamWriter writer = new StreamWriter(path);
                                    writer.Flush();
                                    for (int i = 0; i < cliente.Length; i++)
                                    {
                                        if (i == cliente.Length-1)
                                        {
                                            writer.Write(cliente[i]);
                                        }
                                        else
                                        {
                                            writer.Write(cliente[i] + "\n");
                                        }
                                    }
                                    writer.Close();

                                    StreamReader reader = File.OpenText(path);
                                    clientes = reader.ReadToEnd();
                                    reader.Close();

                                    Array.Clear(cliente, 0, cliente.Length);
                                    cliente = clientes.Split('\n');

                                    if (cliente[pos].Equals("NOMBRE:        " + newName))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Nombre editado correctamente.\n");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("El nombre no pudo ser editado.\n");
                                    }

                                    break;
                                }

                            case "DIRECCION":
                                {
                                    Console.Write("\n¿Cuál es la nueva dirección del cliente?: ");
                                    string newAddress = Console.ReadLine().ToUpper();

                                    // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                    for (int i = pos+1; i < pos+5; i++)
                                    {
                                        if (cliente[i].Substring(0, 9).Equals("DIRECCION"))
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }

                                    cliente[pos] = "DIRECCION:     " + newAddress;

                                    StreamWriter writer = new StreamWriter(path);
                                    writer.Flush();
                                    for (int i = 0; i < cliente.Length; i++)
                                    {
                                        if (i == cliente.Length - 1)
                                        {
                                            writer.Write(cliente[i]);
                                        }
                                        else
                                        {
                                            writer.Write(cliente[i] + "\n");
                                        }
                                    }
                                    writer.Close();

                                    StreamReader reader = File.OpenText(path);
                                    clientes = reader.ReadToEnd();
                                    reader.Close();

                                    Array.Clear(cliente, 0, cliente.Length);
                                    cliente = clientes.Split('\n');

                                    if (cliente[pos].Equals("DIRECCION:     " + newAddress))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("La dirección ha sido editada correctamente.\n");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("La direcció no pudo ser editada.\n");
                                    }

                                    break;
                                }

                            case "CODIGO POSTAL":
                                {
                                    Console.Write("\n¿Cuál es el nuevo código postal del cliente?: ");
                                    string newCP = Console.ReadLine().ToUpper();

                                    // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                    for (int i = pos+2; i < pos+5; i++)
                                    {
                                        if (cliente[i].Substring(0, 13).Equals("CODIGO POSTAL"))
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }

                                    cliente[pos] = "CODIGO POSTAL: " + newCP;

                                    StreamWriter writer = new StreamWriter(path);
                                    writer.Flush();
                                    for (int i = 0; i < cliente.Length; i++)
                                    {
                                        if (i == cliente.Length - 1)
                                        {
                                            writer.Write(cliente[i]);
                                        }
                                        else
                                        {
                                            writer.Write(cliente[i] + "\n");
                                        }
                                    }
                                    writer.Close();

                                    StreamReader reader = File.OpenText(path);
                                    clientes = reader.ReadToEnd();
                                    reader.Close();

                                    Array.Clear(cliente, 0, cliente.Length);
                                    cliente = clientes.Split('\n');

                                    if (cliente[pos].Equals("CODIGO POSTAL: " + newCP))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("El código postal ha sido editado correctamente.\n");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("El código postal no pudo ser editado.\n");
                                    }

                                    break;
                                }

                            case "TELEFONO":
                                {
                                    Console.Write("\n¿Cuál es el nuevo teléfono del cliente?: ");
                                    string newNumberPhone = Console.ReadLine().ToUpper();

                                    // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                    for (int i = pos+3; i < pos+5; i++)
                                    {
                                        if (cliente[i].Substring(0, 8).Equals("TELEFONO"))
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }

                                    cliente[pos] = "TELEFONO:      " + newNumberPhone;

                                    StreamWriter writer = new StreamWriter(path);
                                    writer.Flush();
                                    for (int i = 0; i < cliente.Length; i++)
                                    {
                                        if (i == cliente.Length - 1)
                                        {
                                            writer.Write(cliente[i]);
                                        }
                                        else
                                        {
                                            writer.Write(cliente[i] + "\n");
                                        }
                                    }
                                    writer.Close();

                                    StreamReader reader = File.OpenText(path);
                                    clientes = reader.ReadToEnd();
                                    reader.Close();

                                    Array.Clear(cliente, 0, cliente.Length);
                                    cliente = clientes.Split('\n');

                                    if (cliente[pos].Equals("TELEFONO:      " + newNumberPhone))
                                    {
                                        Console.Clear();
                                        Console.WriteLine("El teléfono ha sido editado correctamente.\n");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("El teléfono no pudo ser editado.\n");
                                    }

                                    break;
                                }

                            case "CORREO":
                                {
                                    Console.Write("\n¿Cuál es el nuevo correo del cliente?: ");
                                    string newEmail = Console.ReadLine().ToUpper();

                                    // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                    for (int i = pos+4; i < pos+5; i++)
                                    {
                                        if (cliente[i].Substring(0, 6).Equals("CORREO"))
                                        {
                                            pos = i;
                                            break;
                                        }
                                    }

                                    cliente[pos] = "CORREO:        " + newEmail;

                                    StreamWriter writer = new StreamWriter(path);
                                    writer.Flush();
                                    for (int i = 0; i < cliente.Length; i++)
                                    {
                                        if (i == cliente.Length - 1)
                                        {
                                            writer.Write(cliente[i]);
                                        }
                                        else
                                        {
                                            writer.Write(cliente[i] + "\n");
                                        }
                                    }
                                    writer.Close();

                                    StreamReader reader = File.OpenText(path);
                                    clientes = reader.ReadToEnd();
                                    reader.Close();

                                    Array.Clear(cliente, 0, cliente.Length);
                                    cliente = clientes.Split('\n');

                                    if (cliente[pos].Equals("CORREO:        " + newEmail))
                                    {
                                        Console.WriteLine("El correo ha sido editado correctamente.\n");
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine("El correo no pudo ser editado.\n");
                                        Console.Clear();
                                    }

                                    break;
                                }

                            default:
                                {
                                    Console.Clear();
                                    break;
                                }
                        }
                    } 
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("El cliente fue eliminado.");

                        Console.Write("¿Desea editar otro? (s/n): ");
                        string op = Console.ReadLine().ToUpper();
                        if (op.Equals("S"))
                        {
                            editClient(path);
                        }
                        Console.Clear();
                    }
                }
                else
                {
                    Console.Write("Cliente no encontrado, ¿desea intentarlo de nuevo? (s/n): ");
                    string op = Console.ReadLine().ToUpper();
                    if (op.Equals("S"))
                    {
                        Console.Clear();
                        editClient(path);
                    }
                }
            }
        }

        private static void deleteClient(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("Ingresa el nombre del cliente que deseas borrar.");
                Console.Write("Nombre: ");
                string delClient = Console.ReadLine().ToUpper();

                StreamReader reader = File.OpenText(path);
                string clientes = reader.ReadToEnd();
                reader.Close();

                string[] cliente = clientes.Split('\n');
                int pos = 0;

                for (int i = 1; i < cliente.Length; i += 8)
                {
                    if (cliente[i].Substring(0, 6).Equals("NOMBRE"))
                    {
                        if (cliente[i].Substring(15).Equals(delClient))
                        {
                            pos = i;
                            break;
                        }
                    }
                }

                if (bool.Parse(cliente[pos + 5].Substring(15)))
                {
                    cliente[pos + 5] = "ESTATUS:       " + "false";

                    StreamWriter writer = new StreamWriter(path);
                    writer.Flush();
                    for (int i = 0; i < cliente.Length; i++)
                    {
                        if (i == cliente.Length - 1)
                        {
                            writer.Write(cliente[i]);
                        }
                        else
                        {
                            writer.Write(cliente[i] + "\n");
                        }
                    }
                    writer.Close();

                    reader = File.OpenText(path);
                    clientes = reader.ReadToEnd();
                    reader.Close();

                    Array.Clear(cliente, 0, cliente.Length);
                    cliente = clientes.Split('\n');

                    if (cliente[pos + 5].Equals("ESTATUS:       " + "false"))
                    {
                        Console.Clear();
                        Console.WriteLine("El cliente ha sido eliminado correctamente.\n");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("El cliente no pudo ser eliminado.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("Este cliente ya está dado de baja.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que puedas borrar :(");
                Console.ReadKey(true);
            }
        }

        private static void viewActiveClients(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------  CLIENTES  ---------------------------------------\n");
                StreamReader reader = File.OpenText(path);
                string clientes = reader.ReadToEnd();
                reader.Close();

                string[] cliente = clientes.Split('\n');

                for (int i = 6; i < cliente.Length; i += 8)
                {
                    if (bool.Parse(cliente[i].Substring(15)))
                    {
                        Console.WriteLine(cliente[i - 5]);
                        Console.WriteLine(cliente[i - 4]);
                        Console.WriteLine(cliente[i - 3]);
                        Console.WriteLine(cliente[i - 2]);
                        Console.WriteLine(cliente[i - 1]);
                        if(i != cliente.Length-1)
                        {
                            Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que mostrar :(");
                Console.ReadKey(true);
            }
        }

        private static void viewAllNameClients(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("--------------------------------------  CLIENTES  ---------------------------------------\n");
                StreamReader reader = File.OpenText(path);
                string clientes = reader.ReadToEnd();
                reader.Close();

                string[] cliente = clientes.Split('\n');

                for (int i = 6; i < cliente.Length; i += 8)
                {
                    Console.WriteLine(cliente[i - 5]);
                    if (i != cliente.Length - 1)
                    {
                        Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que mostrar :(");
                Console.ReadKey(true);
            }
        }

        private static void addProduct(string path)
        {
            Console.Clear();

            Console.Write("Nombre del producto: ");
            string name = Console.ReadLine().ToUpper();
            Console.Write("Descripción: ");
            string description = Console.ReadLine().ToUpper();
            Console.Write("Precio: ");
            string price = Console.ReadLine().ToUpper();
            Console.Write("Cantidad en existencia: ");
            string cantidad = Console.ReadLine().ToUpper();
            Console.Write("Codigo de barras (10 Dígitos): ");
            string codigo = Console.ReadLine().ToUpper();

            Console.WriteLine("\ns: Si | n: No");
            Console.Write("Opción: ");
            string op = Console.ReadLine();

            if (op == "s")
            {
                bool clienteExistente = false;

                if (File.Exists(path))
                {
                    StreamReader lectura = File.OpenText(path);
                    string productos = lectura.ReadToEnd();
                    string[] producto = productos.Split('\n');
                    lectura.Close();

                    for (int i = 1; i < producto.Length; i += 8)
                    {
                        if (producto[i].Substring(15).Equals(name))
                        {
                            clienteExistente = true;
                        }
                    }
                }

                if (!clienteExistente)
                {
                    string hr = "\n-----------------------------------------------------------------------------------------";
                    string newProductInfo = "NOMBRE:        " + name + "\n" +
                                            "DESCRIPCION:   " + description + "\n" +
                                            "PRECIO:        " + price + "\n" +
                                            "CANTIDAD:      " + cantidad + "\n" +
                                            "CODIGO:        " + codigo + "\n" +
                                            "ESTATUS:       true";

                    int id = 1;

                    if (!File.Exists(path))
                    {
                        id = 1;
                        File.WriteAllText(path, "ID:            " + id + "\n" + newProductInfo);

                        Console.Clear();
                        Console.WriteLine("Archivo productos.txt creado correctamente.");
                        Console.WriteLine("Cliente agregado correctamente.\n");
                    }
                    else
                    {
                        StreamReader lectura = File.OpenText(path);
                        string productos = lectura.ReadToEnd();
                        string[] producto = productos.Split('\n');
                        lectura.Close();

                        for (int i = 0; i < producto.Length; i += 8)
                        {
                            if (producto[i].Substring(0, 2).Equals("ID"))
                            {
                                id = int.Parse(producto[i].Substring(15));
                            }
                        }

                        id++;

                        Console.Clear();
                        File.AppendAllText(path, hr + "\nID:            " + id + "\n" + newProductInfo);
                        Console.WriteLine("Producto agregado correctamente.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("El producto ya existe, elimínelo, o edítelo por favor.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            else if (op == "n")
            {
                Console.Clear();
                addClient(path);
            }
            else
            {
                Console.WriteLine("Esa opción no está disponible, se tomará como un NO.");
                Console.ReadKey();
                Console.Clear();
                addClient(path);
            }
        }

        private static void editProduct(string path)
        {
            Console.Clear();

            if (!File.Exists(path))
            {
                Console.Clear();
                Console.Write("Aún no hay nada que modificar :(");
                Console.ReadKey(true);
            }
            else
            {
                StreamReader lectura = File.OpenText(path);
                string productos = lectura.ReadToEnd();
                string[] producto = productos.Split('\n');
                lectura.Close();

                Console.Write("Ingresa el nombre del producto: ");
                string sName = Console.ReadLine().ToUpper();

                bool encontrado = false;
                int pos = 0;

                for (int i = 1; i < producto.Length; i += 8)
                {
                    if (producto[i].Substring(0, 6).Equals("NOMBRE"))
                    {
                        if (producto[i].Substring(15).Equals(sName))
                        {
                            encontrado = true;
                            pos = i;
                            break;
                        }
                        else
                        {
                            encontrado = false;
                        }
                    }
                }

                if (encontrado)
                {
                    Console.Clear();

                    // Muestra el producto a modificar
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine(producto[pos]);
                        pos++;
                    }
                    Console.WriteLine("\n¿Qué deseas editar? (Escribe el nombre del atributo, por ejemplo: 'Nombre')");
                    Console.WriteLine("Para regresar solo presiona enter sin escribir nada.");
                    Console.Write("Atributo: ");
                    string attr = Console.ReadLine().ToUpper();

                    pos -= 4;
                    switch (attr)
                    {
                        case "NOMBRE":
                            {
                                Console.Write("\n¿Cuál es el nuevo nombre del producto?: ");
                                string newName = Console.ReadLine().ToUpper();

                                // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                for (int i = pos; i < pos+5; i++)
                                {
                                    if (producto[i].Substring(0, 6).Equals("NOMBRE"))
                                    {
                                        break;
                                    }
                                }

                                producto[pos] = "NOMBRE:        " + newName;

                                StreamWriter writer = new StreamWriter(path);
                                writer.Flush();
                                for (int i = 0; i < producto.Length; i++)
                                {
                                    if (i == producto.Length - 1)
                                    {
                                        writer.Write(producto[i]);
                                    }
                                    else
                                    {
                                        writer.Write(producto[i] + "\n");
                                    }
                                }
                                writer.Close();

                                StreamReader reader = File.OpenText(path);
                                productos = reader.ReadToEnd();
                                reader.Close();

                                Array.Clear(producto, 0, producto.Length);
                                producto = productos.Split('\n');

                                if (producto[pos].Equals("NOMBRE:        " + newName))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Nombre editado correctamente.\n");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("El nombre no pudo ser editado.\n");
                                }

                                break;
                            }

                        case "DESCRIPCION":
                            {
                                Console.Write("\n¿Cuál es la nueva descripción del producto?: ");
                                string newDescription = Console.ReadLine().ToUpper();

                                // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                for (int i = pos + 1; i < pos+5; i++)
                                {
                                    if (producto[i].Substring(0, 9).Equals("DESCRIPCION"))
                                    {
                                        pos = i;
                                        break;
                                    }
                                }

                                producto[pos] = "DESCRIPCION:   " + newDescription;

                                StreamWriter writer = new StreamWriter(path);
                                writer.Flush();
                                for (int i = 0; i < producto.Length; i++)
                                {
                                    if (i == producto.Length - 1)
                                    {
                                        writer.Write(producto[i]);
                                    }
                                    else
                                    {
                                        writer.Write(producto[i] + "\n");
                                    }
                                }
                                writer.Close();

                                StreamReader reader = File.OpenText(path);
                                productos = reader.ReadToEnd();
                                reader.Close();

                                Array.Clear(producto, 0, producto.Length);
                                producto = productos.Split('\n');

                                if (producto[pos].Equals("DESCRIPCION:   " + newDescription))
                                {
                                    Console.Clear();
                                    Console.WriteLine("La descripción ha sido editada correctamente.\n");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("La descripción no pudo ser editada.\n");
                                }

                                break;
                            }

                        case "PRECIO":
                            {
                                Console.Write("\n¿Cuál es el nuevo precio del producto?: ");
                                string newPrice = Console.ReadLine().ToUpper();

                                // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                for (int i = pos + 2; i < pos+5; i++)
                                {
                                    if (producto[i].Substring(0, 13).Equals("PRECIO"))
                                    {
                                        pos = i;
                                        break;
                                    }
                                }

                                producto[pos] = "PRECIO:        " + newPrice;

                                StreamWriter writer = new StreamWriter(path);
                                writer.Flush();
                                for (int i = 0; i < producto.Length; i++)
                                {
                                    if (i == producto.Length - 1)
                                    {
                                        writer.Write(producto[i]);
                                    }
                                    else
                                    {
                                        writer.Write(producto[i] + "\n");
                                    }
                                }
                                writer.Close();

                                StreamReader reader = File.OpenText(path);
                                productos = reader.ReadToEnd();
                                reader.Close();

                                Array.Clear(producto, 0, producto.Length);
                                producto = productos.Split('\n');

                                if (producto[pos].Equals("PRECIO:        " + newPrice))
                                {
                                    Console.Clear();
                                    Console.WriteLine("El precio ha sido modificado correctamente.\n");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("El precio no pudo ser modificado.\n");
                                }

                                break;
                            }

                        case "CANTIDAD":
                            {
                                Console.Write("\n¿Cuál es la nueva cantidad de productos?: ");
                                string newCantidad = Console.ReadLine().ToUpper();

                                // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                for (int i = pos + 3; i < pos+5; i++)
                                {
                                    if (producto[i].Substring(0, 8).Equals("CANTIDAD"))
                                    {
                                        pos = i;
                                        break;
                                    }
                                }

                                if(int.Parse(newCantidad) <= 0)
                                {
                                    producto[pos] = "CANTIDAD:      " + newCantidad;
                                    producto[pos + 2] = "ESTATUS:       false";
                                }
                                else
                                {
                                    producto[pos] = "CANTIDAD:      " + newCantidad;
                                    producto[pos + 2] = "ESTATUS:       true";
                                }

                                StreamWriter writer = new StreamWriter(path);
                                writer.Flush();
                                for (int i = 0; i < producto.Length; i++)
                                {
                                    if (i == producto.Length - 1)
                                    {
                                        writer.Write(producto[i]);
                                    }
                                    else
                                    {
                                        writer.Write(producto[i] + "\n");
                                    }
                                }
                                writer.Close();

                                StreamReader reader = File.OpenText(path);
                                productos = reader.ReadToEnd();
                                reader.Close();

                                Array.Clear(producto, 0, producto.Length);
                                producto = productos.Split('\n');

                                if (producto[pos].Equals("CANTIDAD:      " + newCantidad))
                                {
                                    Console.Clear();
                                    Console.WriteLine("La cantidad ha sido modificada correctamente.\n");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("La cantidad no pudo ser modificada.\n");
                                }

                                break;
                            }

                        case "CODIGO":
                            {
                                Console.Write("\n¿Cuál es el nuevo código de barras del cliente?: ");
                                string newCodigo = Console.ReadLine().ToUpper();

                                // Encuentra la posicion del atributo a cambiar y la guarda reescribiendo pos
                                for (int i = pos + 4; i < pos+5; i++)
                                {
                                    if (producto[i].Substring(0, 6).Equals("CODIGO"))
                                    {
                                        pos = i;
                                        break;
                                    }
                                }

                                producto[pos] = "CODIGO:        " + newCodigo;

                                StreamWriter writer = new StreamWriter(path);
                                writer.Flush();
                                for (int i = 0; i < producto.Length; i++)
                                {
                                    if (i == producto.Length - 1)
                                    {
                                        writer.Write(producto[i]);
                                    }
                                    else
                                    {
                                        writer.Write(producto[i] + "\n");
                                    }
                                }
                                writer.Close();

                                StreamReader reader = File.OpenText(path);
                                productos = reader.ReadToEnd();
                                reader.Close();

                                Array.Clear(producto, 0, producto.Length);
                                producto = productos.Split('\n');

                                if (producto[pos].Equals("CODIGO:        " + newCodigo))
                                {
                                    Console.WriteLine("El código de barras ha sido editado correctamente.\n");
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine("El código de barras no pudo ser editado.\n");
                                    Console.Clear();
                                }

                                break;
                            }

                        default:
                            {
                                Console.Clear();
                                break;
                            }
                    }
                }
                else
                {
                    Console.Write("Producto no encontrado, ¿desea intentarlo de nuevo? (s/n): ");
                    string op = Console.ReadLine().ToUpper();
                    if (op.Equals("S"))
                    {
                        Console.Clear();
                        editClient(path);
                    }
                }
            }
        }

        private static void deleteProduct(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("Ingresa el nombre del producto que deseas borrar.");
                Console.Write("Nombre: ");
                string delProduct = Console.ReadLine().ToUpper();

                StreamReader reader = File.OpenText(path);
                string productos = reader.ReadToEnd();
                reader.Close();

                string[] producto = productos.Split('\n');
                int pos = 0;

                for (int i = 1; i < producto.Length; i += 8)
                {
                    if (producto[i].Substring(0, 6).Equals("NOMBRE"))
                    {
                        if (producto[i].Substring(15).Equals(delProduct))
                        {
                            pos = i;
                            break;
                        }
                    }
                }

                if (bool.Parse(producto[pos + 5].Substring(15)))
                {
                    producto[pos + 5] = "ESTATUS:       " + "false";

                    StreamWriter writer = new StreamWriter(path);
                    writer.Flush();
                    for (int i = 0; i < producto.Length; i++)
                    {
                        if (i == producto.Length - 1)
                        {
                            writer.Write(producto[i]);
                        }
                        else
                        {
                            writer.Write(producto[i] + "\n");
                        }
                    }
                    writer.Close();

                    reader = File.OpenText(path);
                    productos = reader.ReadToEnd();
                    reader.Close();

                    Array.Clear(producto, 0, producto.Length);
                    producto = productos.Split('\n');

                    if (producto[pos + 5].Equals("ESTATUS:       " + "false"))
                    {
                        Console.Clear();
                        Console.WriteLine("El producto ha sido eliminado correctamente.\n");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("El producto no pudo ser eliminado.\n");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("Este producto ya está dado de baja.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que puedas borrar :(");
                Console.ReadKey(true);
            }
        }

        private static void viewAllProducts(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------  PRODUCTOS  ---------------------------------------\n");
                StreamReader reader = File.OpenText(path);
                string productos = reader.ReadToEnd();
                reader.Close();

                string[] producto = productos.Split('\n');

                for (int i = 6; i < producto.Length; i += 8)
                {
                    if (bool.Parse(producto[i].Substring(15)))
                    {
                        Console.WriteLine(producto[i - 5]);
                        Console.WriteLine(producto[i - 4]);
                        Console.WriteLine(producto[i - 3]);
                        Console.WriteLine(producto[i - 2]);
                        Console.WriteLine(producto[i - 1]);
                        if (i != producto.Length - 1)
                        {
                            Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que mostrar :(");
                Console.ReadKey(true);
            }
        }
        
        private static void viewLowAvailabilityProducts(string path)
        {
            if (File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------  PRODUCTOS  ---------------------------------------\n");
                StreamReader reader = File.OpenText(path);
                string productos = reader.ReadToEnd();
                reader.Close();

                string[] producto = productos.Split('\n');

                for (int i = 6; i < producto.Length; i += 8)
                {
                    if (int.Parse(producto[i-2].Substring(15)) < 3)
                    {
                        Console.WriteLine(producto[i - 5]);
                        Console.WriteLine(producto[i - 4]);
                        Console.WriteLine(producto[i - 3]);
                        Console.WriteLine(producto[i - 2]);
                        Console.WriteLine(producto[i - 1]);
                        if (i != producto.Length - 1)
                        {
                            Console.WriteLine("\n-----------------------------------------------------------------------------------------\n");
                        }
                        else
                        {
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Aún no hay nada que mostrar :(");
                Console.ReadKey(true);
            }
        }
    }
}
