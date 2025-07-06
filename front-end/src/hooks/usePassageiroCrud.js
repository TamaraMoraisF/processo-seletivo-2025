import { useState } from "react";
import axios from "axios";

export function usePassageiroCrud(selectedDuv, setSelectedDuv, setDuvs) {
    const [modalAberto, setModalAberto] = useState(false);
    const [passageiroEmEdicao, setPassageiroEmEdicao] = useState(null);

    const abrirFormularioPassageiro = (p = null, tipo = 1) => {
        setPassageiroEmEdicao(
            p ??
            {
                nome: "",
                nacionalidade: "",
                fotoUrl: "",
                sid: "",
                duvId: selectedDuv.id,
                tipo
            }
        );
        setModalAberto(true);
    };

    const salvarPassageiro = async () => {
        if (passageiroEmEdicao.tipo === 2 && !passageiroEmEdicao.sid?.trim()) {
            alert("SID é obrigatório para tripulantes.");
            return;
        }

        if (passageiroEmEdicao.id) {
            await axios.put(`https://localhost:7204/api/Passageiro/${passageiroEmEdicao.id}`, passageiroEmEdicao);
        } else {
            await axios.post("https://localhost:7204/api/Passageiro", passageiroEmEdicao);
        }

        setModalAberto(false);
        await atualizarDuv();
    };

    const removerPassageiro = async (id) => {
        await axios.delete(`https://localhost:7204/api/Passageiro/${id}`);
        await atualizarDuv();
    };

    const atualizarDuv = async () => {
        const response = await axios.get("https://localhost:7204/api/Duv");
        setDuvs(response.data);
        const atualizada = response.data.find(d => d.id === selectedDuv.id);
        setSelectedDuv(atualizada);
    };

    return {
        modalAberto,
        passageiroEmEdicao,
        setPassageiroEmEdicao,
        abrirFormularioPassageiro,
        salvarPassageiro,
        removerPassageiro,
        setModalAberto
    };
}
