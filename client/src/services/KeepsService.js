import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "../models/Keep.js"


class KeepsService {
    async getKeeps() {
        const response = await api.get('api/keeps')
        logger.log('GOT KEEPS', response.data)
        AppState.keeps = response.data.map(pojo => new Keep(pojo))
    }

    async getKeepById(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        logger.log('GOT KEEP', res.data)
        AppState.activeKeep = new Keep(res.data)
    }

    async createKeep(keepData) {
        const response = await api.post('api/keeps', keepData)
        logger.log('created keep', response.data)
        const newKeep = new Keep(response.data)
        return newKeep
    }

    async getKeepsByAccountId(profileId) {
        const res = await api.get(`api/profiles/${profileId}/keeps`)

        logger.log('got keeps', res.data)
        AppState.keeps = res.data.map(pojo => new Keep(pojo))
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)

        logger.log('got keeps', res.data)
        AppState.keeps = res.data.map(pojo => new Keep(pojo))
    }

    async deleteKeep(keepId) {
        const response = await api.delete(`api/keeps/${keepId}`)
        logger.log('destroyed keep', response.data)
    }

    async searchKeepsByTag(tagName) {
        const res = await api.get('/api/keeps/search', { params: { tagName } });
        return res.data;
        // AppState.keeps = res.data.map(pojo => new Keep(pojo))

    }

}

export const keepsService = new KeepsService()