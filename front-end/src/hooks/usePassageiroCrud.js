import { useState } from "react";
import axios from "axios";

export function usePassageiroCrud(selectedDuv, setSelectedDuv, setDuvs) {
  const [modalAberto, setModalAberto] = useState(false);
  const [passageiroEmEdicao, setPassageiroEmEdicao] = useState(null);
  const [erros, setErros] = useState([]);

  const abrirFormularioPassageiro = (p = null, tipo = 1) => {
    setErros([]);
    setPassageiroEmEdicao(
      p ?? {
        nome: "",
        nacionalidade: "",
        fotoUrl: "",
        sid: "",
        duvGuid: selectedDuv.duvGuid,
        tipo
      }
    );
    setModalAberto(true);
  };

  const salvarPassageiro = async () => {
    try {
      if (passageiroEmEdicao.passageiroGuid) {
        await axios.put(`https://localhost:7204/api/passageiros/${passageiroEmEdicao.passageiroGuid}`, passageiroEmEdicao);
      } else {
        await axios.post("https://localhost:7204/api/passageiros", passageiroEmEdicao);
      }

      setModalAberto(false);
      setErros([]);
      await atualizarDuv();
    } catch (error) {
      console.error("Erro ao salvar passageiro:", error);

      if (error.response?.data?.errors) {
        setErros(error.response.data.errors);
      } else {
        setErros(["Erro inesperado ao salvar passageiro."]);
      }
    }
  };

  const removerPassageiro = async (passageiroGuid) => {
    try {
      await axios.delete(`https://localhost:7204/api/passageiros/${passageiroGuid}`);
      await atualizarDuv();
    } catch (error) {
      console.error("Erro ao remover passageiro:", error);
    }
  };

  const atualizarDuv = async () => {
    const response = await axios.get("https://localhost:7204/api/Duv");
    setDuvs(response.data);
    const atualizada = response.data.find(d => d.duvGuid === selectedDuv.duvGuid);
    setSelectedDuv(atualizada);
  };

  return {
    modalAberto,
    passageiroEmEdicao,
    setPassageiroEmEdicao,
    abrirFormularioPassageiro,
    salvarPassageiro,
    removerPassageiro,
    setModalAberto,
    erros
  };
}
