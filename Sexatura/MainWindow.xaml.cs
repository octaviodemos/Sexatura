using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Specialized;
using static ExercicioTela.MainWindow;
using ExericioTela.Repositorios;
using ExericioTela.Modelos;

namespace ExercicioTela
{
    public partial class MainWindow : Window
    {

        List<Pessoa> pessoas;
        PessoaRepositorio pessoaRepositorio;

        public MainWindow()
        {
            InitializeComponent();
            pessoaRepositorio = new PessoaRepositorio();
            CarregaPessoas();
        }



        private void CarregaPessoas()
        {
            pessoas = pessoaRepositorio.LoadPessoas();
            ParcialListBox.ItemsSource = null;
            ParcialListBox.ItemsSource = pessoas;
        }

        private void CheckFeminino_Checked(object sender, RoutedEventArgs e)
        {
            AlturaText.Focus();
        }

        private void CheckMasculino_Checked(object sender, RoutedEventArgs e)
        {
            AlturaText.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string sexoSelecionado = string.Empty;
            string alturaDigitada = AlturaText.Text;
            double altura;

            if (CheckFeminino.IsChecked == true)
            {
                sexoSelecionado = "feminino";
            }

            if (CheckMasculino.IsChecked == true)
            {
                sexoSelecionado = "masculino";
            }

            if (CheckFeminino.IsChecked == false && CheckMasculino.IsChecked == false)
            {
                MessageBox.Show("Selecione o sexo da pessoa que está cadastrando");
                return;
            }

            if (string.IsNullOrEmpty(alturaDigitada))
            {
                MessageBox.Show("Digite informações de altura corretas");
                AlturaText.Focus();
                return;
            }

            if (double.TryParse(alturaDigitada, out altura) == false || altura <= 0)
            {
                MessageBox.Show("Digite informações de altura corretas");
                AlturaText.Focus();
                return;
            }

            Pessoa novaPessoa = new Pessoa
            {
                Sexo = sexoSelecionado,
                Altura = altura
            };

            pessoaRepositorio.InserirPessoa(novaPessoa);

            MessageBox.Show("Dados salvos");

            CheckFeminino.IsChecked = false;
            CheckMasculino.IsChecked = false;
            AlturaText.Text = string.Empty;
            CarregaPessoas();
            MostrarResultados();
            AlturaText.Focus();

        }


        private void MostrarResultados()
        {
            int totalMulheresMenos170 = 0;
            float alturaHomemMaisAlto = 0;

            foreach (var pessoa in pessoas)
            {
                if (pessoas != null)
                {
                    if (pessoa.Sexo.ToLower() == "feminino" && pessoa.Altura < 1.70)
                    {
                        totalMulheresMenos170++;
                    }
                    if (pessoa.Sexo.ToLower() == "masculino")
                    {
                        if (pessoa.Altura > alturaHomemMaisAlto)
                        {
                            alturaHomemMaisAlto = (float)pessoa.Altura;
                        }
                    }
                }
            }

            ResultsText.Text = $"Quantidade de mulheres com menos de 1.70m: {totalMulheresMenos170}\nAltura do homem mais alto: {alturaHomemMaisAlto} metros";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarResultados();
        }

        private void AlturaText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Button_Click(null, null);
            }
        }

        private void ZerarDados()
        {
            CheckFeminino.IsChecked = false;
            CheckMasculino.IsChecked = false;
            AlturaText.Text = string.Empty;
            ParcialListBox.ItemsSource = null;
            ResultsText.Text = string.Empty;
        }

        private void Reiniciar(object sender, RoutedEventArgs e)
        {
            ZerarDados();
            pessoaRepositorio.Removertudo();

            MessageBox.Show("Reniciado, pode preencher os dados novamente");
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (ParcialListBox.SelectedItem != null)
            {
                Pessoa pessoaSelecionada = (Pessoa)ParcialListBox.SelectedItem;

                pessoaRepositorio.RemoverPessoa(pessoaSelecionada);

                Console.WriteLine($"Pessoa removida: {pessoaSelecionada.Sexo}, {pessoaSelecionada.Altura}m");

                CarregaPessoas();
                MostrarResultados();

                MessageBox.Show("Dados deletados");
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa para deletar.");
            }
        }
    }
}
