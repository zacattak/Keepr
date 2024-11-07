<template>
    <div class="search-container col-12">
        <input type="text" v-model="tagName" class="search-bar" placeholder="Search by tag name"
            @keyup.enter="searchKeeps" />
        <!-- <button @click="searchKeeps">Search</button> -->

        <div class="masonry">
            <div v-for="keep in searchResults" :key="keep.id">
                <KeepComponent :keep="keep" />
            </div>
        </div>
    </div>
</template>

<script>
import { ref } from 'vue';
import { keepsService } from '../services/KeepsService';

export default {
    setup() {
        const tagName = ref('');
        const searchResults = ref([]);

        async function searchKeeps() {
            if (tagName.value) {
                searchResults.value = await keepsService.searchKeepsByTag(tagName.value);
            }
        }

        return {
            tagName,
            searchResults,
            searchKeeps
        };
    }
};
</script>

<style scoped>
.search-container {
    width: 100%;
}

.masonry {
    width: 100%;
    column-count: 3;
    column-gap: 1rem;
}

.masonry>.search-result {
    break-inside: avoid;
    margin-bottom: 1rem;
}


@media (max-width: 768px) {
    .masonry {
        column-count: 2;
    }
}

@media (max-width: 576px) {
    .masonry {
        column-count: 1;
    }
}

.search-bar {
    width: 30%;
    padding: 8px 12px;
    border-radius: 5px;
    border: 1px solid #ccc;
    background-color: #333;
    color: #fff;
    font-size: 16px;
    outline: none;
    transition: background-color 0.3s ease;
}

.search-bar:focus {
    background-color: #555;

}
</style>