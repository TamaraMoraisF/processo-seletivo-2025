import { useState, useEffect } from "react";
import axios from "axios";

export function useNavioCrud() {
  const [navios, setNavios] = useState([]);
  const [modalAberto, setModalAberto] = useState(false);
  const [navioEmEdicao, setNavioEmEdicao] = useState(null);
  const [erros, setErros] = useState([]);

  useEffect(() => {
    carregarNavios();
  }, []);

  const carregarNavios = async () => {
    try {
      const response = await axios.get("https://localhost:7204/api/navios");
      setNavios(response.data);
    } catch (error) {
      console.error("Erro ao buscar navios:", error);
    }
  };

  const abrirFormularioNavio = (navio = null) => {
    setErros([]);
    setNavioEmEdicao(
      navio ?? {
        nome: "",
        bandeira: "",
        imagemUrl: ""
      }
    );
    setModalAberto(true);
  };

  const salvarNavio = async () => {
    try {
      const { navioGuid, nome, bandeira, imagemUrl } = navioEmEdicao ?? {};
      const payload = { nome, bandeira, imagemUrl };

      if (navioGuid) {
        await axios.put(`https://localhost:7204/api/navios/${navioGuid}`, payload);
      } else {
        await axios.post("https://localhost:7204/api/navios", payload);
      }

      setModalAberto(false);
      setErros([]);
      await carregarNavios();
    } catch (error) {
      console.error("Erro ao salvar navio:", error);
      if (error.response?.data?.errors) {
        setErros(error.response.data.errors);
      } else {
        setErros(["Erro inesperado ao salvar navio."]);
      }
    }
  };

  const removerNavio = async (navioGuid) => {
    try {
      await axios.delete(`https://localhost:7204/api/navios/${navioGuid}`);
      await carregarNavios();
    } catch (error) {
      console.error("Erro ao remover navio:", error);
    }
  };

  return {
    navios,
    modalAberto,
    navioEmEdicao,
    setNavioEmEdicao,
    abrirFormularioNavio,
    salvarNavio,
    removerNavio,
    setModalAberto,
    erros
  };
}
