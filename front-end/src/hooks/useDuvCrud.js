import { useState, useEffect } from "react";
import axios from "axios";

export function useDuvCrud() {
    const [duvs, setDuvs] = useState([]);
    const [selectedDuv, setSelectedDuv] = useState(null);
    const [modalAberto, setModalAberto] = useState(false);
    const [duvEmEdicao, setDuvEmEdicao] = useState(null);
    const [erros, setErros] = useState([]);

    useEffect(() => {
        carregarDuvs();
    }, []);

    const carregarDuvs = async () => {
        try {
            const response = await axios.get("https://localhost:7204/api/duvs");
            setDuvs(response.data);
            setSelectedDuv(response.data[0] || null);
        } catch (error) {
            console.error("Erro ao carregar DUVs:", error);
        }
    };

    const abrirFormularioDuv = (duv = null) => {
        setErros([]);

        setDuvEmEdicao(
            duv ?? {
                numero: "",
                dataViagem: new Date().toISOString().split("T")[0],
                navioGuid: "",
                passageiros: [],
                nomePassageiro: "",
                nacionalidade: "",
                fotoUrl: "",
                sid: "",
                tipoPassageiro: 1,
            }
        );

        if (duv) {
            setDuvEmEdicao({
                ...duv,
                navioGuid: duv.navio?.navioGuid ?? "",
                nomePassageiro: "",
                nacionalidade: "",
                fotoUrl: "",
                sid: "",
                tipoPassageiro: 1,
                pessoas: [],
            });
        }

        setModalAberto(true);
    };

    const salvarDuv = async () => {
        try {
            // Validação simples no front para evitar envio com navioGuid vazio
            if (!duvEmEdicao.navioGuid || duvEmEdicao.navioGuid.trim() === "") {
                setErros(["Selecione um navio."]);
                return;
            }

            const payload = {
                numero: duvEmEdicao.numero,
                dataViagem: duvEmEdicao.dataViagem,
                navioGuid: duvEmEdicao.navioGuid,
                passageiros: (duvEmEdicao.pessoas || []).map(p => ({
                    nome: p.nome,
                    nacionalidade: p.nacionalidade,
                    fotoUrl: p.fotoUrl,
                    tipo: p.tipo,
                    sid: p.tipo === 2 ? p.sid : null
                }))
            };

            const response = await axios.post("https://localhost:7204/api/duvs", payload);

            setModalAberto(false);
            setErros([]);
            await carregarDuvs();
        } catch (error) {
            console.error("Erro ao salvar DUV:", error);

            if (error.response?.data?.errors) {
                const validationErrors = Object.values(error.response.data.errors).flat();
                setErros(validationErrors);
            } else {
                setErros(["Erro inesperado ao salvar DUV."]);
            }
        }
    };

    const removerDuv = async (duvGuid) => {
        try {
            await axios.delete(`https://localhost:7204/api/duvs/${duvGuid}`);
            await carregarDuvs();
        } catch (error) {
            console.error("Erro ao remover DUV:", error);
        }
    };

    return {
        duvs,
        selectedDuv,
        setSelectedDuv,
        modalAberto,
        duvEmEdicao,
        setDuvEmEdicao,
        abrirFormularioDuv,
        salvarDuv,
        removerDuv,
        setModalAberto,
        erros
    };
}
