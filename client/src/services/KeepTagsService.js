
import { AppState } from "../AppState.js";
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

    async getKeepTagsByKeepId(keepId) {
        AppState.keepTags = []
        // const response = await api.get(`api/keeps/${keepId}/keepTags`);

        const response = await api.get(`api/keepTags/${keepId}/tags`);

        logger.log('keeps tags had', response.data);


        // const keepTags = Array.isArray(response.data) ? response.data.map(pojo => new TagClone(pojo)) : [new TagClone(response.data)];
        // AppState.keepTags = keepTags;

        const keepTags = response.data.map(pojo => new TagClone(pojo));
        AppState.keepTags = keepTags
    }
}

export const keepTagsService = new KeepTagsService()