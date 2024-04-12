<template>

    <!-- <form @submit.prevent="createVaultKeep()">
        <select v-model="editableVaultKeepData.accountVaultId" class="form-select" aria-label="Select a Vault" required>
            <option v-for="accountVault in accountVaults" :key="accountVault.id" :value="accountVault.id">
               
            </option>
        </select>
    </form> -->
</template>


<script>
import { computed, ref } from 'vue';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { vaultKeepsService } from '../services/VaultKeepsService.js'

export default {
    setup() {
        const editableVaultKeepData = ref({})
        return {
            editableVaultKeepData,
            vaultKeeps: computed(() => AppState.vaultKeeps),
            vaults: computed(() => AppState.vaults),
            accountVaults: computed(() => AppState.accountVaults),
            async createVaultKeep() {
                try {
                    await vaultKeepsService.createVaultKeep(editableVaultKeepData.value)
                    editableVaultKeepData.value = {}
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>