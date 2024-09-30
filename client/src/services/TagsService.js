import { AppState } from "../AppState.js";

import { Tag } from "../models/Tag.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class TagsService {
    async createTag(tagData) {
        const response = await api.post('api/tags', tagData)
        logger.log('created tag', response.data)
        const tag = new Tag(response.data)
        return tag
    }
}

export const tagsService = new TagsService