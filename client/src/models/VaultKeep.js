import { Keep } from './Keep.js'

export class VaultKeep extends Keep {
    super(data) {
        this.id = data.id
        this.creatorId = data.creatorId
        this.keepId = data.keepId
        this.vaultId = data.vaultId
    }

}