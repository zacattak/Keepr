<template>
    <div class="search-container">
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
.masonry {
    column-count: 3;
    column-gap: 1rem;
}

.search-container {
    position: relative;
    width: 100%;
    max-width: 300px;
    margin-bottom: 20px;
    display: flex;
    align-items: center;
}

/* Styling for the search input field */
.search-bar {
    width: 100%;
    padding: 8px 12px;
    /* Adds inset spacing for placeholder text */
    border-radius: 5px;
    border: 1px solid #ccc;
    background-color: #333;
    /* Dark gray background */
    color: #fff;
    /* White text */
    font-size: 16px;
    outline: none;
    transition: background-color 0.3s ease;
}

/* Lighter background color when the search bar is focused */
.search-bar:focus {
    background-color: #555;
    /* Lighter gray when active */
}
</style>