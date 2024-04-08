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
}

export const keepsService = new KeepsService()