
import { TagClone } from "../models/TagClone.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepTagsService {
    async createKeepTag(keepTagData) {
        const response = await api.post('api/keepTags', keepTagData)
        logger.log('created keepTag', response.data)
        const keepTag = new TagClone(response.data)


        return keepTag
    }
}

export const keepTagsService = new KeepTagsService()